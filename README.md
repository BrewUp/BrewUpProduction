# BrewUpProduction

Split production API from monolith

## Getting started

### 1. Spin up _Event Store_

#### Windows

By commandline:

~~~powershell
.\EventStore.ClusterNode.exe --insecure --enable-external-tcp
~~~

Default port is `2113`.

### 2. Spin up _MongoDB_

#### MongoDB on localhost

##### Windows
Run from commandline (without authentication):

~~~powershell
.\mongod.exe --port 27017 --dbpath data --noauth
~~~

`data` is the folder where mongo db are supposed to be.  
Specify a folder of choice; BrewUp will create the dbs it needs at runtime.  

ConnectionString: `mongodb://localhost`

Run with Docker:
ConnectionString: `mongodb://citizix:S3cret@localhost`

#### MongoDB on Atlas

Connection string:
`mongodb://BrewUp:BrewUp!2022@ac-nhigxof-shard-00-00.477s2hi.mongodb.net:27017,ac-nhigxof-shard-00-01.477s2hi.mongodb.net:27017,ac-nhigxof-shard-00-02.477s2hi.mongodb.net:27017/?ssl=true&replicaSet=atlas-7e16qu-shard-0&authSource=admin&retryWrites=true&w=majori`

### 3. Setup Azure

### 4. Run the solution

Walk into `BrewUpProduction` folder and run the project:

~~~powershell
cd BrewUpProduction
dotnet run
~~~
