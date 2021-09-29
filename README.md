# TinyFateTeller

## !Important! Afin que le client Python démarre correctement, la solution BirthEntryServices.sln doit être exécuté.

### Introduction

Ce projet est divisé en 4 parties :

1. _Dans le dossier « BirthEntryServices »_ :

La solution Visual Studio nommée « BirthEntryServices » comprenant les 2 projets suivants :

1. **BirthEntryServiceDAO_WCF**
2. **BirthService_REST**

3. _Dans le dossier « UserClient-Java »_

Le projet Java nommé : « **UserClient** ».

1. _Dans le dossier « AdminClient-Python »_

Le répertoire « **AdminClient**. Le fichier de démarrage étant : « main.py », à la racine du répertoire.

1. Le fichier « dates_of_birth.sql » dans le dossier « Database ».

#

# BirthEntryServiceDAO_WCF - C# .NET Framework

- Service WCF qui sert de service « DAO ». C&#39;est ce service qui exécute le code lié à la base de données.

- Ce service est référencé dans le projet « **BirthService_REST** » en tant que « **Connected Service** »
- C&#39;est le service avec lequel le client admin « **AdminClient** » communique.
- Réalisé avec .NET Framework 4.8

#

# BirthService_REST - C# .NET CORE 5

- Service REST qui permet de d&#39;obtenir « la destinée » d&#39;un individu selon sa date de naissance. Permet aussi d&#39;envoyer une requête au service « BirthEntryServiceDAO_WCF » pour qu&#39;il ajoute de nouvelles insertions d&#39;utilisateurs dans la base de données.
- Ce service est utilisé dans le projet « **UserClient** » à l&#39;aide de la classe **HttpURLConnection**
- Les URL servant à appeler les méthodes sont :

Prendre note que le numéro de port pourrait être différent :

1. [http://localhost:5000/api/Birth/getActivity/?BirthDate=1973-11-02](http://localhost:5000/api/Birth/getActivity/?BirthDate=1973-11-02)
2. [http://localhost:5000/api/Birth/AddUserToDatabase/?](http://localhost:5000/api/Birth/AddUserToDatabase/?) + query

query : Username, Hostname, LocalIP, PublicIP, EntryDate,BirthDate

#

# UserClient - Java

**« self-explanatory»**

# AdminClient Python

**« self-explanatory»**

# Schéma global du travail réalisé

![screenshot_of_schema_global](ss3.png)

