X = 0; Y = 1; HEAD = 0

class Rope():
    def __init__(self,s,l): self.k = [s[:] for _ in range(0,l)]

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

tailPositions1 = set(); tailPositions2 = set(); rope1 = Rope([0,0], 2); rope2 = Rope([0,0], 10)
for line in open("input.txt", "r").read().split('\n'):
    direction, steps = line.split(' ')
    for _ in range(0, int(steps)):
        rope1.moveHead(direction); rope2.moveHead(direction)
        tailPositions1.add((rope1.k[-1][X], rope1.k[-1][Y])); tailPositions2.add((rope2.k[-1][X], rope2.k[-1][Y]))

print('Task 1: ' + str(len(list(tailPositions1))))
print('Task 2: ' + str(len(list(tailPositions2))))