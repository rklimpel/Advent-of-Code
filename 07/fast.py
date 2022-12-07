import json

lines = open("input.txt", "r").read().split('\n')

allFileSizes = 0
filesystem = {}
currentPath = ""
for line in lines:
    if "$" in line:
        if "cd" in line:
            path = line[line.index("cd")+3:]
            if path == "..":
                currentPath = currentPath[currentPath.rfind("/"):]
            else:
                if len(currentPath) == 0 or currentPath[-1] == "/" or path == "/":
                    currentPath += path
                else:
                    currentPath += "/" + path
                if currentPath not in filesystem:
                    filesystem[currentPath] = {}
    elif line[0].isnumeric():
        size, file = line[:line.index(" ")], line[line.index(" ")+1:]
        allFileSizes += int(size)
        filesystem[currentPath][file] = int(size)

print(json.dumps(filesystem,sort_keys=True, indent=4))

dirSizes = {}
for directory in  list(filesystem.keys()):
    dirSize = 0
    for file in list(filesystem[directory].keys()):
        dirSize += filesystem[directory][file]
    dirSizes[directory] = dirSize

rDirSizes = {}
for directory in dirSizes.keys():
    #print(directory + " => " + str(dirSizes[directory]))
    rDirSizes[directory] = dirSizes[directory]
    for dir2 in dirSizes.keys():
        if directory != dir2 and dir2.startswith(directory):
            #print("+ " + str(dir2) + " - " + str(dirSizes[dir2]))
            rDirSizes[directory] += dirSizes[dir2]
    #print("Result: " + directory + " => " + str(rDirSizes[directory]))


totalSize = 0
for key in rDirSizes.keys():
    if rDirSizes[key] <= 100000:
        totalSize += rDirSizes[key]

print(rDirSizes["/"])
print(allFileSizes)
print(totalSize)

# 1700687 number is too low
# 1791835 number is too High
# 1879303 number is too High