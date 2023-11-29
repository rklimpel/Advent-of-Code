class Section:
    def __init__(self, start, end):
        self.start = start
        self.end = end
    
    def contains(self, section):
        if self.start <= section.start and self.end >= section.end:
            print("contains!")
            return True
        else:
            return False

    def overlap(self, section):
        if ((self.start <= section.start and self.end >= section.start) 
            or (self.start <= section.end and self.end >= section.end)
            or (section.start <= self.start and section.end >= self.start)
            or (section.start <= self.end and section.end >= self.end)):
            return True
        else:
            return False

    def __str__(self):
     return "start: " + str(self.start) + ", end: " + str(self.end)


lines = open("input.txt", "r").read().split("\n")
sum = 0
for line in lines:
    print("-----")
    pairs = line.split(',')
    firstSection = Section(int(pairs[0].split('-')[0]), int(pairs[0].split('-')[1]))
    secondSection = Section(int(pairs[1].split('-')[0]), int(pairs[1].split('-')[1]))
    print(firstSection)
    print(secondSection)
    if firstSection.overlap(secondSection):
        sum += 1
print(sum)
    
