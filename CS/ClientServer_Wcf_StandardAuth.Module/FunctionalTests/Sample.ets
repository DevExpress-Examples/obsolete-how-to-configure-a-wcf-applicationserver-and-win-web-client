;#DropDB ClientServer_Wcf_StandardAuthEasyTest

#Application ClientServer_Wcf_StandardAuth.Win
#Application ClientServer_Wcf_StandardAuth.Web

*Action Navigation(User)

*ProcessRecord
 User Name = [DomainUserName]

*OptionalAction Edit
 
*FillForm
 User Name = Test User
 
*Action Cancel

*OptionalAction Yes

*CheckFieldValues
 User Name = [DomainUserName]
