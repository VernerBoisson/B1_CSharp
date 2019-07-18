# B1_TP-Twitter

## Objectif
J'avais pour projet en cours de C#/dotnet de créer un script qui manipule une API et de proposer un certain nombre de fonctionnalité.

## Présentation
Mon script permet de récupérer les images que poste un utilisateur Twitter et les sauvegarder, il permet d'enregistrer les images d'une recherche sur twitter et il permet aussi de s'inscrire à des concours twitter en "rt" et "follow" les personnes qui mettent certains mots comme "concours" dans leur message.

## Configuration
Il y a un a fichier à configurer avant de l'utiliser "appSetting.json".

les champs suivant permettent l'identifacation :
* consumerKey
* consumerSecret
* accessToken
* accessTokenSecret

Les champs qui suivent permettent sont pour les fonctionnalités :
* path : permet de choisir le dossier où seront sauvegarder les images
* users : sont utilisateur desquels on veut sauvegarder les images
* keywords : sont les recheches desquels on veut sauvegarder les images
* concours : sont les mots à chercher pour participer aux concours
