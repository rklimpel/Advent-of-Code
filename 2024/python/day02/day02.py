def check_save(levels):
    increase = all(levels[i] < levels[i+1] and abs(levels[i] - levels[i+1]) <= 3 for i in range(len(levels) - 1))
    decrease = all(levels[i] > levels[i+1] and abs(levels[i] - levels[i+1]) <= 3 for i in range(len(levels) - 1))
    return any([increase, decrease])

def get_permutations_by_removing_one(lst):
    return [lst[:i] + lst[i+1:] for i in range(len(lst))]

report = open("input", "r").read().split("\n")
sum = 0 

for levels in report:
    levels = levels.split(" ")
    levels = list(map(int, levels))
    if check_save(levels): sum += 1

print(sum)

# TASK 2

sum2 = 0 

for levels in report:
    levels = levels.split(" ")
    levels = list(map(int, levels))
    if check_save(levels): sum2 += 1
    else:
        perms = get_permutations_by_removing_one(levels)
        if any(check_save(perm) for perm in perms):
            sum2 += 1

print(sum2)


