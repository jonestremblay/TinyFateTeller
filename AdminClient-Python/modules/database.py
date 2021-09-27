import datetime as dt
import mysql.connector as mysql
from requests import Session
from zeep import Client
from zeep.transports import Transport

# Adding WSDL reference here
wsdl = "http://localhost:8733/Design_Time_Addresses/BirthServiceDAO_WCF/UserCatalogService/?wsdl"
session = Session()
session.verify = False
transport = Transport(session=session)
client = Client(wsdl, transport=transport)

mydb = mysql.connect(
  host="sql5.freemysqlhosting.net",
  user="sql5440579",
  password="6Ps5JxudLG",
  database="sql5440579"
)

### mode = {'full','year','month','day', 'hour', 'minute', 'second'}
def get_entry_date(entryDate, mode):
  dateStr = entryDate.split(' ')[0]
  time = entryDate.split(' ')[1]
  year = dateStr.split('-')[0]
  month = dateStr.split('-')[1]
  day = dateStr.split('-')[2]
  hour = time.split(':')[0]
  minute = time.split(':')[1]
  second = time.split(':')[2]
  date = dt.datetime(int(year), int(month), int(day), int(hour), int(minute), int(second), 0, tzinfo=None)

  switch = {
    "fullDateObj" : date,
    "fullDateStr" : dateStr,
    "year" : year,
    "month" : month,
    "day" : day,
    "hour" : hour,
    "minute" : minute,
    "second" : second
  }
  return switch.get(mode, 
      "Unknown mode. modesList : {'fullDateObj', 'fullDateStr', 'year','month','day', 'hour', 'minute', 'second'} ")

def get_all_rows(dbResults):
  data = []
  rowCounter = 0
  print(dbResults)
  if dbResults is not None:
    for rows in dbResults:
      row = []
      print("Rows ((( ", rows, ")))") 
      try :
        entry_date = get_entry_date(str(rows.EntryDate), 'fullDateObj')
        entry_date_str = entry_date.strftime("%Y-%b-%d|%H:%M:%S %p")
        row.append(str(rows.EntryDate))
        #row.append(entry_date_str) # 5
        row.append(str(rows.HostName)) # 2
        row.append(str(rows.LOCAL_IP_Address)) # 3
        row.append(str(rows.PUBLIC_IP_Address)) # 4
        row.append(str(rows.UserName)) # 1
        row.append(str(rows.BirthDate)) # 6
      except AttributeError:
        print("function get_allRows() --> attribute error !")
      data.append(row)
  return data

def get_result(dbResult):
  data = []
  result = []
  try:
    entry_date = get_entry_date(str(dbResult.EntryDate), 'fullDateObj')
    entry_date_str = entry_date.strftime("%Y-%b-%d|%H:%M:%S %p")
    result.append(str(dbResult.EntryDate))
    result.append(str(dbResult.HostName)) # 2
    result.append(str(dbResult.LOCAL_IP_Address)) # 3
    result.append(str(dbResult.PUBLIC_IP_Address)) # 4
    result.append(str(dbResult.UserName)) # 1
    result.append(str(dbResult.BirthDate)) # 6
  except IndexError:
    print("function get_result() --> No results has been found !")
  data.append(result)
  return data

def get_all():
  users = client.service.GetAllUsers()
  return get_all_rows(users)

def get_by_username(username):
  user = client.service.GetByUsername(username)
  # print(user)
  return get_all_rows(user)
  
def get_by_username_pattern(username):
  users = client.service.GetByUsernamePattern(username)
  # print(user)
  return get_all_rows(users)


def get_all_service():
  users = client.service.GetAllUsers()
  #print(users)
  return get_all_rows(users)

def get_all_by_birth_date(birthDate):
  users = client.service.GetAllByDateOfBirth(birthDate)
  return get_all_rows(users)

def get_all_before_birth_date(birthDate):
  users = client.service.GetAllBeforeDateOfBirth(birthDate)
  return get_all_rows(users)

def get_all_after_birth_date(birthDate):
  users = client.service.GetAllAfterDateOfBirth(birthDate)
  return get_all_rows(users)

def get_all_between_two_entry_date(minDate, maxDate):
  users = client.service.GetUsersByDateEntryRange(minDate, maxDate)
  return get_all_rows(users)

def get_all_in_entry_date_range(when):
  users = client.service.GetAllByEntryDateTimeline(when)
  return get_all_rows(users)

def get_all_by_public_ip(publicIP):
  users = client.service.GetAllByPublicIP(publicIP)
  return get_all_rows(users)

