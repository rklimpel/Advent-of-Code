lines = open("input", "r").read().split("\n")
listA = []
listB = []
for line in lines:
    values = line.split("   ")
    listA.append(int(values[0]))
    listB.append(int(values[1]))

listA.sort()
listB.sort()

# print(listA)
# print("---")
# print(listB)

sum = 0

for x in range(len(listA)):
    sum += abs(listA[x] - listB[x])

print("Part 1: " + str(sum))

sim = 0

for x in listA:
    sim += x * listB.count(x)

print("Part 2: " + str(sim))
