
import re
import os
def listToString(s):
    # initialize an empty string
    str1 = ""
    # traverse in the string
    for ele in s:
        str1 += ele
    # return string
    return str1

fileinput = input("Please enter the absolute file path location" + "\n" + "for example: C:\\Users\\username\\Desktop\\Logs.txt" + ": " )
try:
    file = open(fileinput,"r")
    data_from_file = file.read()
    file.close()
    dashes = "-------------------------------------------------------------------------------"
    list_of_logs = data_from_file.split(dashes)

    i = 1 # because [0] is a '' from split
    found_logs = []

    while i < len(list_of_logs):
        counter = 1 # counter is 1, because we append first log into the found_logs list
        found_logs.append(list_of_logs[i]) # append first log into the found logs list
        log_message = re.findall(r'^[^<][a-zA-Z].+[^>]',list_of_logs[i],re.MULTILINE)
        string_log_message = listToString(log_message)

        j = i + 1 # next log
        while j < len(list_of_logs):
            if string_log_message in list_of_logs[j]:
                counter += 1
                list_of_logs.pop(j)
            else:
                j+=1  # next

        found_logs[-1] = "\n" +"Number of occurrences " + str(counter) + found_logs[-1] + dashes + "\n"
        list_of_logs.pop(i)
        if counter == 1 or counter == 2:
            found_logs.pop()

    print(listToString(found_logs))
except FileNotFoundError:
    print("File does not exist")
