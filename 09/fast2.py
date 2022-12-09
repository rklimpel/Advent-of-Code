X = 0
Y = 1
HEAD = 0

def printGrid(rope, includePositions = False):
    for y in range(0,GRID+1):
        for x in range(0,GRID+1):
            printed = False
            if y == GRID and x == GRID: 
                printed = True
            elif y == GRID: 
                print((str(x) if len(str(x)) == 1 else str(x)[1]) + ' ', end='')
                printed = True
            elif x == GRID: 
                print(str(y), end="")
                printed = True
            else:
                for i in range(0,len(rope.k)):
                    if (rope.k[i]) == [x,y] and not printed: 
                        print(str(i) + ' ', end = '')
                        printed = True
            if not printed: print('. ', end='')
        print()
    if includePositions:
        print("Head: " + str(rope.h))
        print("Tail: " + str(rope.t))

def printVisited(s,v):
    for y in range(0,GRID+1):
        for x in range(0,GRID+1):
            printed = False
            if s[X] == x and s[Y] == y: 
                printed = True
                print('S', end='')
            for i in v:
                if i[X] == x and i[Y] == y and not printed: 
                    printed = True
                    print('X', end='')
            if not printed: print('.', end='')
        print()

class Rope():

    def __init__(self,s,l):
        self.k = [s[:] for _ in range(0,l)]; self.s = s

    def moveHead(self, d):
        if d == "U": self.k[HEAD][Y] -= 1
        elif d == "D": self.k[HEAD][Y] += 1
        elif d == "L": self.k[HEAD][X] -= 1
        elif d == "R": self.k[HEAD][X] += 1
        self.moveKnots()

    def knotsTouching(self, knot1, knot2):
        for x in range(-1, 2):
            for y in range(-1, 2):
                if knot1[X] + x == knot2[X] and knot1[Y] + y == knot2[Y]: return True
        return False

    def moveKnots(self):
        for i in range(0, len(self.k)-1):
            if not self.knotsTouching(self.k[i], self.k[i+1]): 
                if self.k[i][X] > self.k[i+1][X]: self.k[i+1][X] += 1
                elif self.k[i][X] < self.k[i+1][X]: self.k[i+1][X] -= 1
                if self.k[i][Y] > self.k[i+1][Y]: self.k[i+1][Y] += 1
                elif self.k[i][Y] < self.k[i+1][Y]: self.k[i+1][Y] -= 1

START = [150,150]; GRID = START[X]*2
tailPositions = set()
rope = Rope(START, 10)

lines = open("input.txt", "r").read().split('\n')
for line in lines:
    direction, steps = line.split(' ')
    for _ in range(0, int(steps)):
        rope.moveHead(direction)
        tailPositions.add((rope.k[-1][X], rope.k[-1][Y]))

# printVisited(START, tailPositions)
print(len(list(tailPositions)))