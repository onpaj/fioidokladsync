# Azure Function to download bank statements from FIO bank to iDoklad
- `SyncLast` function - time trigger - Loads new statements every 10 minutes
- `Sync custom` function - http trigger - Allows custom load of statements within defined date range
```
POST http://xxxxxx/SyncCustom

{
	"DateFrom":"2019-05-13",
	"DateTo":"2019-05-13"
}
```


# Setup
- requires env. variables:
  - `FIO_API_KEY` - Provided by FIO bank in account backoffice
  - `IDOKLAD_CLIENTID` - provided by iDoklad 
  - `IDOKLAD_CLIENT_SECRET` - provided by iDoklad 