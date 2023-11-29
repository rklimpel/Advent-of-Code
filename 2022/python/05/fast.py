SOLVE = 1

lines = open("input.txt", "r").read().split("\n")
configLines = lines[:8]
stacks = [[],[],[],[],[],[],[],[],[]]
for configLine in configLines:
    splitted = [configLine[i:i+4] for i in range(0, len(configLine), 4)]
    for i in range(len(splitted)):
        clearedSplit = splitted[i].replace(' ', '').replace('[','').replace(']','')
        if clearedSplit != '':
            stacks[i] += clearedSplit

for stack in stacks:
    stack.reverse()

moveLines = lines[10:]

for moveLine in moveLines:
    moveNumbers = [int(s) for s in moveLine.split() if s.isdigit()]
    count, start, end = moveNumbers
    movingItems = stacks[start-1][-count:]
    stacks[start-1] = stacks[start-1][:len(stacks[start-1])-count]
    if SOLVE == 1:
        movingItems.reverse()
    stacks[end-1] += movingItems

print(''.join([stack[-1] for stack in stacks]))