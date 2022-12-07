TOTAL_FILESYSTEM = 70000000
UPDATE_SIZE = 30000000

filesystem = {}
allPaths = []
currentPath = []

def nested_set(dic, keys, value):
    for key in keys[:-1]: dic = dic.setdefault(key, {})
    dic[keys[-1]] = value

def getFromDict(dataDict, mapList):    
    for k in mapList: dataDict = dataDict[k]
    return dataDict

def recursiveSize(d):
    size = 0
    for k in d.keys():
        if isinstance(d[k], dict):
            size += recursiveSize(d[k])
        else:            
            size += d[k]
    return size

for line in open("input.txt", "r").read().split('\n'):
    if "$ cd" in line:
        path = line[line.index("cd")+3:]
        if path == "..": currentPath = currentPath[:-1]
        else: 
            currentPath.append(path)
            if not currentPath in allPaths:   
                allPaths.append(currentPath[:])
    elif line[0].isnumeric():
        size, file = line[:line.index(" ")], line[line.index(" ")+1:]
        nested_set(filesystem, currentPath + [file], int(size))

dirSizes = {}
for path in allPaths: dirSizes['/'.join(path)] = recursiveSize(getFromDict(filesystem, path))
neededSpace = UPDATE_SIZE - (TOTAL_FILESYSTEM - recursiveSize(filesystem['/']))

print('Task 1: ' + str(sum([dirSizes[k] if dirSizes[k] <= 100000 else 0 for k in dirSizes.keys()])))
print('Task 2: ' + str(min([dirSizes[k] if dirSizes[k] >= neededSpace else TOTAL_FILESYSTEM for k in dirSizes.keys()])))