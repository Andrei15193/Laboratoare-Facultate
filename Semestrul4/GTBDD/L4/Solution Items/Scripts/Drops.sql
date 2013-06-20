if exists(select name from sys.objects where type = 'TR' and name = 'ccValidationTrigger')
    drop trigger ccValidationTrigger

if exists(select name from sys.objects where type = 'FN' and name = 'ValidateOccAgainstOcc')
    drop function ValidateOccAgainstOcc
if exists(select name from sys.objects where type = 'FN' and name = 'ValidateOccAgainstCc')
    drop function ValidateOccAgainstCc
if exists(select name from sys.objects where type = 'FN' and name = 'ValidateOcc')
    drop function ValidateOcc

if exists(select name from sys.objects where type = 'P' and name = 'StartTransaction')
    drop procedure StartTransaction
if exists(select name from sys.objects where type = 'P' and name = 'SubmitOperation')
    drop procedure SubmitOperation
if exists(select name from sys.objects where type = 'P' and name = 'CommitTransaction')
    drop procedure CommitTransaction
if exists(select name from sys.objects where type = 'P' and name = 'RollbackTransaction')
    drop procedure RollbackTransaction

if exists(select name from sys.objects where type = 'U' and name = 'TransactionOperations')
    drop table TransactionOperations
if exists(select name from sys.objects where type = 'U' and name = 'Transactions')
    drop table Transactions

if exists(select name from sys.objects where type = 'U' and name = 'Note')
    drop table Note
