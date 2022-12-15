# API Lego Indexor

### Utilisation
1. Cloner le répertoire.
2. Utiliser le dockerfile du projet afin de lancer les conteneurs nécessaires.
3. Si les conteneurs ne marchent pas, ouvrir une base de données PostgreSQL en local et lancer l'API via dotnet CLI ou un IDE.
4. Ouvrir un reverse proxy (nginx ou apache) redirigeant les requêtes http vers l'instance local de ASP.Net.

### Dépendences
Pour utiliser l'application, vous aurez besoin des dépendences suivantes:
- [Application mobile](https://github.com/BenocxX/lego-indexor-app)
- [Équipement nécessaire](https://github.com/BenocxX/lego-indexor-raspberrypi)
- [Modèle d'intelligence artificielle](https://github.com/BenocxX/lego-indexor-machine-learning)
