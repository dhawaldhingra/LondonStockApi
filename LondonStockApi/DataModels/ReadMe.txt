

This folder contains data objects directly related to the relational database(LondonStockApi.db).
Not to be confused with data objects present in Models folder which contains data objects consumed and exposed by the API.
Segregating them has below benefits-
	1. Any changes in database schema don't result in changes in API's external interface and vice-versa
	2. Database objects are not always directly compatible with the external interface so this allows loose coupling
	3. Not exposing database schema directly to the external parties adds an additional layer of security