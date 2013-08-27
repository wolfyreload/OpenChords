import glob
import datetime
from sets import Set
import time

UniqueIPAddresses = set()

def countUniqueVisitorsForDay(day):
	allEntries = []
	logFiles = glob.glob(day+"*.log")
	for i in range(0, len(logFiles)):
		file = open(logFiles[i]).read()
		logEntries = file.split("\n")
		for j in range(0, len(logEntries)):
			entry = logEntries[j]
			if len(entry) > 3:
				allEntries.append(entry)
	
	uniqueEntries = set()
	for i in range(0, len(allEntries)):
		trimmedEntry = allEntries[i]
		trimmedEntry = trimmedEntry[0:15]
		# print trimmedEntry
		uniqueEntries.add(trimmedEntry)
		UniqueIPAddresses.add(trimmedEntry)
	#print uniqueEntries
	return len(uniqueEntries)

#print the number of unique users each day
def countUsersForEachDay():
	#endDate = datetime.date.today()
	#startDate = endDate + datetime.timedelta(days=-31)
	startDate = datetime.date(2013,2,1)
	endDate = datetime.date(2013,12,30)
	
	maxConcurrent = 0
	d = startDate
	while d <= endDate:
		numberOfUsers = countUniqueVisitorsForDay(str(d))
		if numberOfUsers > maxConcurrent:
			maxConcurrent = numberOfUsers
		if numberOfUsers > 0:
			print d, numberOfUsers 
		d = d + datetime.timedelta(days=1) 
	
	print "maximum concurrent users", maxConcurrent

def printUniqueIpAddresses():
	file = open('uniqueip.txt', 'w')
	
	temp = []
	for address in UniqueIPAddresses:
		temp.append(address)
	temp.sort()
	
	for address in temp:
		file.write(address + '\n')
	file.close()
	return
	
countUsersForEachDay()
print "unique IP Addresses", len(UniqueIPAddresses)
printUniqueIpAddresses()
