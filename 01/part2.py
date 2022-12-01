import sys

calorieList = []
currentElfCalories = 0
for line in open('input1.txt','r').readlines():
    if line == '\n':
        calorieList.append(currentElfCalories)
        currentElfCalories = 0
    else:
        currentElfCalories += int(line)

topThreeCalories = 0
for i in range(3):
    topThreeCalories += max(calorieList)
    print("Top " + str(i+1) + " calories = " + str(max(calorieList)))
    calorieList.remove(max(calorieList))

print("Top Three total Calories: " + str(topThreeCalories))