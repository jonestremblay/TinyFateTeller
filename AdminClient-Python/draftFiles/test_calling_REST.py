# I want to keep that near me.
""" # import db_test as db
import requests

class ApiError(Exception):
    pass

response = requests.get('https://localhost:44313/api/imc', verify=False) 
if response.status_code != 200:
    # This means something went wrong.
    print("ERROR : ", 'GET /api/imc {}'.format(response.status_code))
    raise ApiError('GET /api/imc {}'.format(response.status_code))
for item in response.json():
    print(item) """