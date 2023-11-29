import heapq, string

HEIGHTS = dict(zip(string.ascii_lowercase[:26], [item for item in range(1,27)]))

class PriorityQueue:
    def __init__(self):
        self.elements = []
    
    def empty(self):
        return not self.elements
    
    def put(self, item, priority):
        heapq.heappush(self.elements, (priority, item))
    
    def get(self):
        return heapq.heappop(self.elements)[1]

class Map():

    def get_height(self, location):
        (x, y) = location
        if self.grid[y][x] == 'S': return HEIGHTS['a']
        elif self.grid[y][x] == 'E': return HEIGHTS['z']
        else: return HEIGHTS[self.grid[y][x]]

    def __init__(self, grid):
        self.width = len(grid[0])
        self.height = len(grid)
        self.grid = grid

    def check_bounds(self, location):
        (x, y) = location
        return 0 <= x < self.width and 0 <= y < self.height
    
    def check_passable(self, location_old, location_new):
        return self.get_height(location_new) - self.get_height(location_old) <= 1

    def neighbors(self, location):
        (x, y) = location
        neighbors = [(x+1, y), (x-1, y), (x, y-1), (x, y+1)]
        results = filter(self.check_bounds, neighbors)
        results = filter(lambda x: self.check_passable(location, x), results)
        return results

    def cost(self, from_location, to_location): return 1
    

def heuristic(a, b):
    (x1, y1) = a; (x2, y2) = b
    return abs(x1 - x2) + abs(y1 - y2)

def a_star(graph, start, end):
    queue = PriorityQueue()
    queue.put(start, 0)
    came_from = {}; cost = {}
    came_from[start] = None; cost[start] = 0
    while not queue.empty():
        current = queue.get()
        if current == end: break 
        for next in graph.neighbors(current):
            new_cost = cost[current] + graph.cost(current, next)
            if next not in cost or new_cost < cost[next]:
                cost[next] = new_cost
                priority = new_cost + heuristic(next, end)
                queue.put(next, priority)
                came_from[next] = current
    
    return came_from, cost

def reconstruct_path(came_from, start, end) :
    current = end; path = []
    if end not in came_from: return []
    while current != start:
        path.append(current)
        current = came_from[current]
    path.append(start) 
    path.reverse()
    return path

def print_map(map):
    for line in map:
        print(''.join(line))

lines = open("input.txt", "r").read().split('\n')
mapMatrix = []
start = (0,0)
end = (0,0)
all_a_starts = []

for i in range(len(lines)):
    if 'S' in lines[i]: 
        start = (lines[i].index('S'), i)
    if 'E' in lines[i]: end = (lines[i].index('E'), i)
    mapMatrix.append(list(lines[i]))

came_from, cost = a_star(Map(mapMatrix), start, end)
path = reconstruct_path(came_from, start, end)

print_map(mapMatrix)
print(start)
print(end)
print(path)
print('Task 1: ' + str(len(path)-1))

for y in range(len(mapMatrix)):
    for x in range(len(mapMatrix[0])):
        if mapMatrix[y][x] == 'a' or mapMatrix[y][x] == 'S':
            all_a_starts.append((x, y))

print("Found possible starting points: " + str(len(all_a_starts)))

all_path_lengths = []

for start_point in all_a_starts:
    came_from, cost = a_star(Map(mapMatrix), start_point, end)
    path = reconstruct_path(came_from, start_point, end)
    if len(path) != 0:
        all_path_lengths.append(len(path)-1)

print("Task 2: " + str(min(all_path_lengths)))