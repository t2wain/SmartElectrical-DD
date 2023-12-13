## EntityFramework Core Implementation for the Data Dictionary Schema

# DbContext

This project defines an abstract read-only DbContext (DictDbContext) to connect to the data dictionary schemas of the Intergraph Smart Electrical (SEL) application. The specific implementation will inherit DictDbContext to specify the data provider and the database connection information. Each SEL application deployment has three (3) separate data dictionary schemas:

	1. Site data dictionary (SPSITE)
	2. Plant data dictionary (SPAPLANT)
	3. Electrical data dictionary (SPEL)

All these three (3) data dictionary schemas have the same database object definitions but contain different set of data. The SPSITE and the SPAPLANT optionally can be shared across mutiple SEL application deployment.

# Primary Classes

The ItemSQL class is the main class that contains the entry methods, GetItemJoin() and GetItemObjRelJoin(), to perform the calculations. These methods return the data object results, ItemJoin and ObjectJoin, which contain methods to output the data to file. 

With ItemJoin data object, you can output the item's properties (PrintItemAttributions), or the item's full SQL statement (PrintAllSQL) to retrieve those properties.

With ObjectJoin data object, you can print the item's relations (PrintRelations), or the item's full SQL statements (PrintAllSQL) to retrieve the properties from those relations.