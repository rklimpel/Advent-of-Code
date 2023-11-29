X = 1
cycles = []
lines = open("input.txt", "r").read().split('\n')
for instruction in lines:
    if instruction == "noop":
        cycles.append(0)
    else:
        v = instruction.split(' ')[1]
        cycles.append(0)
        cycles.append(int(v))

showRegister = [20,60,100,140,180,220]
siganlStrengths = []
hPosition = 0

for i in range(0, len(cycles) + 1):
    if hPosition - X < 2 and hPosition -X > -2:
        print('X', end='')
    else:
        print('.',end='')
    
    if hPosition == 39:
        print()
        hPosition = 0
    else:
        hPosition += 1

    if i+1 in showRegister:
        siganlStrengths += [(i+1)*X]
        # print("Cycle " + str(i+1) + ": " + str(X) + " => " + str((i+1)*X))

    if i == len(cycles):
        break
    X += cycles[i]

print('Task 1: ' + str(sum(siganlStrengths)))