
class Cube():

    def __init__(self, position):
        x, y, z = position
        self.position = position
        self.x = x
        self.y = y
        self.z = z

    def get_neighbors_list(self):
        x, y, z = self.x, self.y, self.z
        return [
            (x + 1, y, z),
            (x - 1, y, z),
            (x, y + 1, z),
            (x, y - 1, z),
            (x, y, z + 1),
            (x, y, z - 1),
        ]

    def __str__(self) -> str:
        return "x: " + str(self.x) + " | y: " + str(self.y) + " | z: " + str(self.z)

    def __eq__(self, other: object) -> bool:
        if isinstance(other, self.__class__):
            return self.position == other.position
        else:
            return False

lines = open("input.txt", "r").read().split('\n')
cubes = []
for line in lines:
    coords = list(map(int, line.split(',')))
    cubes.append(Cube((coords[0], coords[1], coords[2])))

sum = 0
for cube in cubes:
    b = 6
    for neighbor in cube.get_neighbors_list():
        if Cube(neighbor) in cubes:
            b -= 1
    sum += b

print(sum)
