import requests
import json

url = 'https://zirk.eu/Manchaud/manchaud.php'
payload = {'name': "userTest",
		   'score': 42,
		   'version': "1.1.10"}

r = requests.post(url, data=json.dumps(payload))