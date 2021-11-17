# IgniteMobile
A small system implementing OAuth2.0 and OpenId Connect via IdentityServer4

the system applications and services

## IdentitySuite

The identity provider that implements IdentityServer4, protects all the Resource Servers, Authenticates and authorizes the clinets to login and access the resources.

## Subscriptions Service

A resource server, built in using .Net5 WepAPI, serves the operations related to the system's subscribers

## Reporting Service

A confidential server-side clinet and a resource server, built in using .Net5 WepAPI, used to generate the reports which reads the required subscribers data from  
Subscriptions Service

## IgniteMobile Admin

A public frontend clinet, built in using Blazer Server, acts an backoffice for the employees to perform various operations including generating reports


