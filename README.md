# NetworkCoverageAPI
 WebAPI developed for the Papernest challenge.
 
## Goal
A small api project that we can request with a textual address request and retrieve 2G/3G/4G network coverage for each operator (if available) in the response.

It uses :
 - https://www.data.gouv.fr/s/resources/monreseaumobile/20180228-174515/2018_01_Sites_mobiles_2G_3G_4G_France_metropolitaine_L93.csv The file provide a list of network coverage measure. Each line have the provider (20801 = Orange, 20810 = SFR, 20815 = Free, 20820 = Bouygue, source), Lambert93 geographic coordinate (X, Y) and network coverage for 2G, 3G and 4G. This file is stored in the project in "Utils\NetworkCoverageCSV.csv" and can be updated as oong as the updated file follow the same rules (Operateur;X;Y;2G;3G;4G).

 - https://adresse.data.gouv.fr/api This API allow you to retrieve :
  1. Address detail from a query address (the insee code, geographic coordinates, etc.)
  2. Do reverse geographic search (from longitude and latitude, retrieve an address).

## QuickStart
 1. Clone/download and then open the repository.

 2. Build the project
   > dotnet build

 3. Run the project
   > dotnet run

## API Address
 Base url : 
 > https://localhost:5001/networkcoverage/

 Parameters :
 > q=*address to check*

 Example : 
 > https://localhost:5001/networkcoverage/?q=42+rue+papernest+75011+Paris

## Response

>[
>    {
>        "brandName": "SFR",
>        "is2GCovered": true,
>        "is3GCovered": true,
>        "is4GCovered": true,
>        "globalApproximation": 205.625
>    }
>]

- brandName (string)    : Name of the network provider
- is2GCovered (boolean) : If 2G is covered or not.
- is3GCovered (boolean) : If 3G is covered or not.
- is4GCovered (boolean) : If 4G is covered or not.
- globalApproximation   : The difference between the address in the query and the provider data exact address. 
