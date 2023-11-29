import json

TOTAL_FILESYSTEM = 70000000
UPDATE_SIZE = 30000000

def nested_set(dic, keys, value):
    for key in keys[:-1]: dic = dic.setdefault(key, {})
    dic[keys[-1]] = value

def getFromDict(dataDict, mapList):    
    for k in mapList: dataDict = dataDict[k]
    return dataDict

lines = open("input.txt", "r").read().split('\n')

allFileSizes = 0
filesystem = {}
allPaths = []
currentPath = []
for line in lines:
    if "$" in line:
        if "cd" in line:
            path = line[line.index("cd")+3:]
            if path == "..": currentPath = currentPath[:-1]
            else: 
                currentPath.append(path)
                if not currentPath in allPaths:   
                    allPaths.append(currentPath[:])
    elif line[0].isnumeric():
        size, file = line[:line.index(" ")], line[line.index(" ")+1:]
        allFileSizes += int(size)
        nested_set(filesystem, currentPath + [file], int(size))

# print(json.dumps(filesystem, sort_keys=True, indent=4))

def recursiveSize(d):
    size = 0
    for k in d.keys():
        if isinstance(d[k], dict):
            size += recursiveSize(d[k])
        else:            
            size += d[k]
    return size

dirSizes = {}
for path in allPaths:
    pathString = '/'.join(path)
    print('/'.join(path) + ": " + str(recursiveSize(getFromDict(filesystem, path))))
    dirSizes[pathString] = recursiveSize(getFromDict(filesystem, path))

task1Size = 0
for path in dirSizes.keys():
    # print(path + ": " + str(dirSizes[path]))
    if dirSizes[path] <= 100000:
        task1Size += dirSizes[path]

print(task1Size)

totalFileSize = recursiveSize(filesystem['/'])
freeSpace = TOTAL_FILESYSTEM - totalFileSize
neededSpace = UPDATE_SIZE - freeSpace

smallest = TOTAL_FILESYSTEM
for path in dirSizes.keys():
    if dirSizes[path] >= neededSpace:
        if dirSizes[path] <= smallest:
            smallest = dirSizes[path]
print('Task 2: ' + str(smallest))
