import sys

calorieList = []
currentElfCalories = 0
for line in open('input.txt','r').readlines():
    if line == '\n':
        calorieList.append(currentElfCalories)
        currentElfCalories = 0
    else:
        currentElfCalories += int(line)

print("Most Calories: " + str(max(calorieList)))
print("That guy with a lot of calories is elf number " + str(calorieList.index(max(calorieList)) +1 ))

topThreeCalories = 0
for i in range(3):
    topThreeCalories += max(calorieList)
    print("Top " + str(i+1) + " calories = " + str(max(calorieList)))
    calorieList.remove(max(calorieList))

print("Top Three total Calories: " + str(topThreeCalories))