import maya.cmds as cmds

LGtargetList=["browDown_L","browDown_R","browInnerUp","browOuterUp_L","browOuterUp_R",
"cheekPuff","cheekSquint_L","cheekSquint_R","eyeBlink_L","eyeBlink_R",
"eyeLookDown_L","eyeLookDown_R","eyeLookIn_L","eyeLookIn_R","eyeLookOut_L","eyeLookOut_R",
"eyeLookUp_L","eyeLookUp_R","eyeSquint_L","eyeSquint_R","eyeWide_L","eyeWide_R",
"jawForward","jawLeft","jawOpen","jawRight","mouthDimple_L","mouthDimple_R",
"mouthFrown_L","mouthFrown_R","mouthFunnel","mouthLeft","mouthLowerDown_L","mouthLowerDown_R",
"mouthPress_L","mouthPress_R","mouthPucker","mouthRight",
"mouthRollLower","mouthRollUpper","mouthShrugLower","mouthShrugUpper","mouthSmile_L","mouthSmile_R",
"mouthStretch_L","mouthStretch_R","mouthUpperUp_L","mouthUpperUp_R","noseSneer_L","noseSneer_R"]

class LGAI():
	"""docstring for LGAI"""
	def __init__(self):
		pass
		
	def readCSVfile(self,csvFile):
	    import csv
	    f= open(csvFile,'r')
	    csvR=csv.reader(f)
	    csvData = []
	    for line in csvR:
	        csvData.append(line)
	    f.close
	    return csvData

	def setFps(self,fps):
	    time = {'game':15,'film':24,'pal':25,'ntsc':30,'show':48,'palf':50,'ntscf':60}
	    frame = time.keys()[time.values().index(fps)]
	    cmds.currentUnit(time=frame)
	    return fps

	def getBSLGtargetList(self,BSName):
	    bsTargetAll = cmds.aliasAttr(BSName,q=1)
	    bsTargetName = []
	    for bsItem in bsTargetAll:
	        if bsItem.count('weight'):
	            pass
	        else:
	            bsTargetName.append(bsItem)
	    return bsTargetName

def setKey_csv(csvFile,fps=30):
    LGAIc=LGAI()
    # make locator and addAttr (same target list)
    bsList = cmds.ls(type='blendShape')
    # read csv file
    keyData = LGAIc.readCSVfile(csvFile)
    # set custom fps
    LGAIc.setFps(fps)
    # set duration
    cmds.playbackOptions(minTime=0,maxTime=len(keyData))
    cmds.playbackOptions(ast=0,aet=len(keyData))
    cmds.currentTime(0)
    # set key from 'csv file'
    for bs in bsList:
    	targets = LGAIc.getBSLGtargetList(bs)
    	for n in range(len(LGtargetList)):
    		cmds.setKeyframe(bs,at=LGtargetList[n],v=0,t=0)
	        for frame in range(len(keyData)):
	            if LGtargetList[n] in targets:
	                cmds.setKeyframe(bs,at='{0}'.format(LGtargetList[n]),v=float(keyData[frame][n]),t=frame+1)
    # notice finish
    print 'DONE'

    
    