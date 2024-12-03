import re

def multiply_string(str):
    pattern = r"\d+"
    numbers = re.findall(pattern, str)
    return int(numbers[0]) * int(numbers[1])

calculations = open("input", "r").read()

pattern = r"mul\(\d+,\d+\)"
matches = re.findall(pattern, calculations)

result = 0
for match in matches:
    result += multiply_string(match)

print(result)

# PART 2

dos = [0] + [match.end() for match in re.finditer(r"do\(\)", calculations)]
donts = [match.end() for match in re.finditer(r"don\'t\(\)", calculations)]
matches_with_indices = [(match.group(), match.start(), match.end()) for match in re.finditer(pattern, calculations)]

def find_next_smaller(numbers, target):
    # print("Numbers: " + str(numbers))
    # print("Target: " + str(target))
    return max((num for num in numbers if num <= target), default=-1)

def check_is_enabled(match_start):
    # print("Match: " + str(match))
    nextDo = find_next_smaller(dos, match_start)
    # print("Next Do(): " + str(nextDo))
    nextDont = find_next_smaller(donts, match_start)
    # print("Next Don't(): " + str(nextDont))
    enabled = match_start - nextDo <= match_start - nextDont
    # print("Enabled = " + str(enabled))
    return enabled

sum2 = 0
for match in matches_with_indices:
    if check_is_enabled(match[1]):
        sum2 += multiply_string(match[0])

print(sum2)

