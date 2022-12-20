
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
            Cube((x + 1, y, z)),
            Cube((x - 1, y, z)),
            Cube((x, y + 1, z)),
            Cube((x, y - 1, z)),
            Cube((x, y, z + 1)),
            Cube((x, y, z - 1)),
        ]

    def __str__(self) -> str:
        return "x: " + str(self.x) + " | y: " + str(self.y) + " | z: " + str(self.z)

    def __eq__(self, other: object) -> bool:
        if isinstance(other, self.__class__):
            return self.position == other.position
        else:
            return False

def find_outside_cubes(start, cubes):
    outside = []
    queue = [start]
    visited = [start]
    while queue:
        cube = queue.pop(0)
        outside.append(cube)
        for neighbor in cube.get_neighbors_list():
            if neighbor not in visited:
                visited.append(neighbor)
                if neighbor.x >= 0 and neighbor.x <= max_x + 1 and neighbor.y >= 0 and neighbor.y <= max_y and \
                    neighbor.z >= 0 and neighbor.z <= max_z and neighbor not in cubes and neighbor not in outside:
                        queue.append(neighbor)
    return outside

lines = open("input.txt", "r").read().split('\n')
cubes = []
for line in lines:
    coords = list(map(int, line.split(',')))
    cubes.append(Cube((coords[0], coords[1], coords[2])))

max_x = max(map(lambda cube: cube.x, cubes))
max_y = max(map(lambda cube: cube.y, cubes))
max_z = max(map(lambda cube: cube.z, cubes))


print("Find Outside Cubes...")
outside_cubes = find_outside_cubes(Cube((max_x, max_y, max_z)), cubes)

print("Done.")
fill_cubes = []

print("Add inner Cubes to Cube List...")
for x in range(0, max_x + 1):
    for y in range(0, max_y + 1):
        for z in range(0, max_z + 1):
            fill_cube = Cube((x, y, z))
            if fill_cube not in cubes and fill_cube not in outside_cubes:
                    fill_cubes.append(fill_cube)
print("Done.")


print("Count up cube edges...")
sum = 0
for cube in cubes:
    b = 6
    for neighbor in cube.get_neighbors_list():
        if neighbor in cubes or neighbor in fill_cubes:
            b -= 1
    sum += b
print("Done.")

print(sum)


# 2546 too low
# 2692 is wrong
# 4348 too high