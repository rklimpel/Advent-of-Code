X = 0
Y = 1

def printGrid(rope, includePositions = False):
    for y in range(0,GRID+1):
        for x in range(0,GRID+1):
            if y == GRID and x == GRID: None
            elif y == GRID: print(str(x) + ' ', end='')
            elif x == GRID: print(str(y), end="")
            elif rope.h == [x,y]: print('H ', end = '')
            elif rope.t == [x,y]: print('T ', end = '')
            elif rope.s == [x,y]: print('S ', end = '')
            else: print('. ', end='')
        print()
    if includePositions:
        print("Head: " + str(rope.h))
        print("Tail: " + str(rope.t))

class Rope():

    def __init__(self,startPosition):
        self.t = startPosition[:]
        self.h = startPosition[:]
        self.s = startPosition[:]

    def moveHead(self, d):
        if d == "U": self.h[Y] -= 1
        elif d == "D": self.h[Y] += 1
        elif d == "L": self.h[X] -= 1
        elif d == "R": self.h[X] += 1
        self.moveTail()

    def knotsTouching(self):
        for x in range(-1, 2):
            for y in range(-1, 2):
                if self.h[X] + x == self.t[X] and self.h[Y] + y == self.t[Y]: return True
        return False

    def moveTail(self):
        if not self.knotsTouching(): 
            if self.h[X] > self.t[X]: self.t[X] += 1
            elif self.h[X] < self.t[X]: self.t[X] -= 1
            if self.h[Y] > self.t[Y]: self.t[Y] += 1
            elif self.h[Y] < self.t[Y]: self.t[Y] -= 1



START = [5,5]; GRID = START[X]*2
tailPositions = []
rope = Rope(START)

printGrid(rope, includePositions=True)

lines = open("input.txt", "r").read().split('\n')
for line in lines:
    direction, steps = line.split(' ')
    for _ in range(0, int(steps)):
        rope.moveHead(direction)
        tailPositions.append(str(rope.t))
        # print("-----")
        # printGrid(rope, includePositions=True)

print(len(list(set(tailPositions))))

# 5739 too low
# 5740 too low