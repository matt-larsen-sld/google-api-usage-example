import os
import googleapiclient.discovery
from google.oauth2 import service_account


BASE_DIR = os.path.dirname(os.path.abspath(__file__))
INPUT_DIR = os.path.join(BASE_DIR, "input")

USER_DOMAIN = "connorgp.com"

# Help documentation
# https://github.com/googleapis/google-api-python-client/blob/master/docs/oauth-server.md
# https://github.com/googleapis/google-api-python-client/blob/master/docs/start.md
# https://developers.google.com/identity/protocols/oauth2/service-account#delegatingauthority


SCOPES = [
    "https://www.googleapis.com/auth/admin.directory.user",
]
SERVICE_ACCOUNT_FILE = os.path.join(
    INPUT_DIR, "inactiveusermanagement-19a5f95e18b5.json"
)

credentials = service_account.Credentials.from_service_account_file(
    SERVICE_ACCOUNT_FILE, scopes=SCOPES
)

delegated_credential = credentials.with_subject("matt.larsen@connorgp.com")

admin_service = googleapiclient.discovery.build("admin", "directory_v1", credentials=delegated_credential)
response = admin_service.users().list(domain=USER_DOMAIN).execute()

print(f"{len(response['users'])} Google users found for '{USER_DOMAIN}'.")
