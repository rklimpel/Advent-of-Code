from enum import Enum
X = 0; Y = 1;
LOGGING = True

class Direction(Enum):
    NORTH = 0,
    SOUTH = 1,
    EAST = 2,
    WEST = 3

class Elf():

    def __init__(self, position):
        self.position = position
        self.next_position = None 

    def __str__(self):
        return str(self.__dict__)

    def calc_next_position(self, others, direction_list):
        if self.check_around(others):
            self.next_position = self.position
        else:
            x,y = self.position
            for direction in direction_list:
                if direction == Direction.NORTH:
                    if self.check_north(others): 
                        print("Elf at " + str(self.position) + " north is free!")
                        self.next_position = (x, y-1)
                        break
                elif direction == Direction.SOUTH:
                    if self.check_south(others): 
                        print("Elf at " + str(self.position) + " south is free!")
                        self.next_position = (x, y+1)
                        break
                elif direction == Direction.EAST:
                    if self.check_west(others): 
                        print("Elf at " + str(self.position) + " west is free!")
                        self.next_position = (x-1,y)
                        break
                else:
                    if self.check_east(others): 
                        print("Elf at " + str(self.position) + " east is free!")
                        self.next_position = (x+1,y)
                        break

    def move(self):
        if self.next_position != None:
            print("Elf moves from " + str(self.position) + " to " + str(self.next_position))
            self.position = self.next_position
            self.next_position = None

    def check(self, spots, others):
        blocked_spots = []
        for spot in spots:
            this_spot_blocked = False
            for other in others: 
                if other.position == spot: this_spot_blocked = True
            if check_spot_out_of_bounds(spot):
                return False
            blocked_spots.append(this_spot_blocked)
        if True in blocked_spots:
            return False
        else:
            return True

    # return True if no one is there
    def check_around(self, others):
        directions = [
            (self.position[X] - 1, self.position[Y] - 1), 
            (self.position[X], self.position[Y] - 1),
            (self.position[X] + 1, self.position[Y] - 1),
            (self.position[X] - 1, self.position[Y] + 1), 
            (self.position[X], self.position[Y] + 1),
            (self.position[X] + 1, self.position[Y] + 1),
            (self.position[X] + 1, self.position[Y]),
            (self.position[X] - 1, self.position[Y]),
        ]
        for spot in directions:
            for other in others:
                if other.position == spot: return False
            if check_spot_out_of_bounds(spot): return False
        return True

    def check_north(self, others):
        directions = [
            (self.position[X] - 1, self.position[Y] - 1), 
            (self.position[X], self.position[Y] - 1),
            (self.position[X] + 1, self.position[Y] - 1)
        ]
        return self.check(directions, others)

    def check_east(self, others):
        directions = [
            (self.position[X] + 1, self.position[Y] - 1), 
            (self.position[X] + 1, self.position[Y]),
            (self.position[X] + 1, self.position[Y] + 1)
        ]
        return self.check(directions, others)

    def check_south(self, others):
        directions = [
            (self.position[X] - 1, self.position[Y] + 1), 
            (self.position[X], self.position[Y] + 1),
            (self.position[X] + 1, self.position[Y] + 1)
        ]
        return self.check(directions, others)

    def check_west(self, others):
        directions = [
            (self.position[X] - 1, self.position[Y] - 1), 
            (self.position[X] - 1, self.position[Y]),
            (self.position[X] - 1, self.position[Y] + 1)
        ]
        return self.check(directions, others)

def check_spot_out_of_bounds(spot):
    x,y = spot
    return x < 0 or y < 0 or x > WIDTH or y > HEIGHT


def print_map(h, w, elfs):
    for y in range(0,h):
        for x in range(0,w):
            printed = False
            for elf in elfs:
                if elf.position == (x,y):
                    print('#', end='')
                    printed = True
            if printed == False:
                print('.', end='')
        print()

def rect_tiles(w,h,elfs):
    y_s = []
    x_s = []
    for elf in elfs:
        y_s.append(elf.position[1])
        x_s.append(elf.position[0])
    l = min(x_s)
    r = max(x_s)
    t = min(y_s)
    b = max(y_s)
    if LOGGING:
        print(l)
        print(r)
        print(t)
        print(b)
    empty_tiles = ((r-l)+1) * ((b-t)+1)
    print(empty_tiles)
    for y in range(t,b+1):
        for x in range(l,r+1):
            for elf in elfs:
                if elf.position == (x,y): empty_tiles -= 1
    return empty_tiles

lines = open("input-sample.txt", "r").read().split('\n')
map = []
for line in lines:
    map.append([*line])

HEIGHT = len(map)
WIDTH = len(map[0])
    
elfs = []
for y in range(len(map)):
    for x in range(len(map[0])):
        if map[y][x] == '#':
            elfs.append(Elf((x, y)))

print("Before Start:")
print_map(HEIGHT, WIDTH, elfs)

for i in range(0,10):
    print("Round " + str(i+1))

    directions = []
    if i % 4 == 0: directions = [Direction.NORTH, Direction.SOUTH, Direction.EAST, Direction.WEST]
    elif i % 4 == 1: directions = [Direction.SOUTH, Direction.EAST, Direction.WEST, Direction.NORTH]
    elif i % 4 == 2: directions = [Direction.EAST, Direction.WEST, Direction.NORTH, Direction.SOUTH]
    elif i % 4 == 3: directions = [Direction.WEST, Direction.NORTH, Direction.SOUTH, Direction.EAST]

    for elf in elfs:
        others = [x for x in elfs if x != elf]
        elf.calc_next_position(others, directions)
    
    for elf in elfs: 
        others = [x for x in elfs if x != elf]
        for other in others:
            if elf.next_position == other.next_position:
                elf.next_position = elf.position
                other.next_position = other.position

    for elf in elfs:
        elf.move() 

    print_map(HEIGHT, WIDTH, elfs)

print(rect_tiles(WIDTH, HEIGHT, elfs))