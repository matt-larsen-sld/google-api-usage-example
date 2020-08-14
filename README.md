# List Google Users Example

This project shows some basic functionality for listing users of a domain in a G-Suite account using the Python and .Net SDKs.  This README will not give all the necessary elements to accomplish that task.  Rather, it will point to the Google documentation or community information and highlight the elements from those document that seem to need extra emphasis for understanding how to work with the Google API and SDKs. 

## Create an App

This is a *server to server* application example and will make use of a *service account*.  Most of the Google documentation referes to the OAuth 2 authentication workflow where your application takes on the identity of the user interacting with the application.  This is not the case for this situation, so you need to look for specifics about using a service account for authentication when the authentication topic is relevant.

The first step is to create a project in the Google API console.  The [documentation](https://developers.google.com/admin-sdk/directory/v1/guides/prerequisites) for the prerequisites is fairly easy to follow.

After the project has been created you'll enable the *Admin SDK*.  The documentation for this is not clear.  From the *Dashboard* for the project click on the "Library" in the left navigation column.  Search for the "Admin SDK" and "enable" it.  

## Create a service account credential for the app

Next, you need a [service account credential](https://developers.google.com/identity/protocols/oauth2#serviceaccount) for the project.  There are different paths in the UI you can follow to accomplish this depending on what already exists for the project.  Essentially, you need to create a service account and download the credential file.

* The service account doesn't need any roles.
* It seems like a good idea to grant at least another administrator on the Google account admin access to the service account.
* Click on the service account after it's been created to view it's settings / properties.
* Enable "G Suite Domain-wide Delegation" for the service account.  More on this is given below in the section about global delegation.
* Add a "*JSON*" Key for the service account
  * A "json" file should be downloaded by your browser.  This file has secret information in it that will be needed for your application to perform authentication.  **_Save this file and keep it safe_**.  
* Copy the `Unique ID` for the service account.  **It's needed later** for the *domain wide delegation*. 

## Scope

This example is using the ["*Directory API*"](https://developers.google.com/admin-sdk/directory) to [list user accounts in your domain](https://developers.google.com/admin-sdk/directory/v1/guides/manage-users#get_all_domain_users).  To do that you need to *authorize* your service account to use the API endpoint by delegating the service account the permissions to impersonate a user account with sufficient permissions to carry out that activity in your G Suite account.  

First, you need to determine which scopes you intend to use with your application.  The scope is a URL that you can find a couple of different ways.  

* The *Directory API* lists the scopes on its [authorize](https://developers.google.com/admin-sdk/directory/v1/guides/authorizing) page.  In this case, the `https://www.googleapis.com/auth/admin.directory.user` or the `https://www.googleapis.com/auth/admin.directory.user.readonly` scope would work.  
  * For example, the "*Reports API*" lists two scopes on its [authorization](https://developers.google.com/admin-sdk/reports/v1/guides/authorizing) page, one for audit and one for usage.  
* The *Reference* page for an API lists the resources that the API has endpoints for.  The "Directory API" [reference](https://developers.google.com/admin-sdk/directory/v1/reference) page has an option for `Users`.  
  * The `list` request page allows you to use Google's API Explorer to make requests to the API endpoint that list users.  There are 3 scopes given on this page as well.  These should match what the *Guides* - *authorize* page for the API resource show.

    The API Explorer is useful to try different API endpoints, but don't let the authentication for it cause confusion.  It is its own application that Google has published and uses the OAuth 2 workflow to grant the application access to the data through the user's account interacting with the page.  

## Grant global delegation for the service credential

Next you [authorize the delegation of permissions to your service account](https://developers.google.com/identity/protocols/oauth2/service-account#delegatingauthority), for the scopes you've chosen, for the G Suite account.  

* In your G Suite admin console select `Security` from the hamburger menu.
* Select "*App Access Control*"
* Click "*Manage Domain Wide Delegation*"
* Add a new delegation
  * Enter the `Unique ID` for your service account created and recorded earlier
  * Enter the scope or scopes within the API you've determined you project is going to use.
    * The scope used here **MUST MATCH** the scope used in your application code.  .Net uses a collection of class fields to set the scope.  Python uses a list of strings.
    * In your code you're going to designate which user account the service account will impersonate to carry out its actions.
  * Click the *Authorize* button.  

    This may take some time to go into effect.  See the *Note* [here](https://developers.google.com/identity/protocols/oauth2/service-account#delegatingauthority).

## .Net


## Python


