lines = open("input.txt", "r").read().split('\n')

treeMap = [list(line) for line in lines]
visibleTrees = 0
for y in range(len(treeMap)):
    for x in range(len(treeMap[0])):
        size = treeMap[y][x]
        visible = False
        if y == 0 or y == len(treeMap) or x == 0 or x == len(treeMap[0]):
            visible = True
        else:
            visibleTop, visibleBottom, visibleLeft, visibleRight = True, True, True, True
            for y2 in range(0, y): visibleTop = False if treeMap[y2][x] >= size else visibleTop
            for y2 in range(y+1,len(treeMap)): visibleBottom = False if treeMap[y2][x] >= size else visibleBottom
            for x2 in range(0, x): visibleLeft = False if treeMap[y][x2] >= size else visibleLeft
            for x2 in range(x+1, len(treeMap[0])): visibleRight = False if treeMap[y][x2] >= size else visibleRight
            visible = True if visibleTop or visibleBottom or visibleLeft or visibleRight else visible
        visibleTrees += 1 if visible else 0

print('Task 1: ' + str(visibleTrees))

scenicScores = [[0 for i in range(len(treeMap[k]))] for k in range(len(treeMap))]
for y in range(len(treeMap)):
    for x in range(len(treeMap[0])):
        size = treeMap[y][x]
        scoreLeft, scoreRight, scoreUp, scoreDown = 0, 0, 0, 0
        for y2 in reversed(range(0, y)): 
            if treeMap[y2][x] < size: 
                scoreUp += 1
            elif treeMap[y2][x] >= size: 
                scoreUp += 1
                break
        if y != len(treeMap):
            for y2 in range(y+1,len(treeMap)): 
                if treeMap[y2][x] < size:
                    scoreDown += 1
                elif treeMap[y2][x] >= size:
                    scoreDown += 1
                    break
        for x2 in reversed(range(0, x)): 
            if treeMap[y][x2] < size:
                scoreRight += 1
            else:
                scoreRight += 1
                break
        if x != len(treeMap[0]):
            for x2 in range(x+1, len(treeMap[0])): 
                if treeMap[y][x2] < size:
                    scoreLeft += 1
                else:
                    scoreLeft += 1
                    break
        scenicScores[y][x] = scoreUp * scoreDown * scoreLeft * scoreRight

print('Task 2: ' + str(max(map(max, scenicScores))))

