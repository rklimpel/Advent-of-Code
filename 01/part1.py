import sys

calorieList = []
currentElfCalories = 0
for line in open('input1.txt','r').readlines():
    if line == '\n':
        calorieList.append(currentElfCalories)
        currentElfCalories = 0
    else:
        currentElfCalories += int(line)

for i in range(len(calorieList)):
    print("Elf " + str(i + 1) + " colories: " + str(calorieList[i]))
print("Most Calories: " + str(max(calorieList)))
print("Thats Elf number " + str(calorieList.index(max(calorieList) + 1)))