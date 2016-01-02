# CustomClaimsSample

*RolesGroups* <-> Controllers e.g. Home
*Roles* <-> Action e.g. Contact
*Claims* <-> Permission e.g. Home_Contact

Claims are added to users for the actions they are permitted to acces which are stored in the form of roles and grouped by role groups only for viewing purpose.
Access is prevented by using a custom attribute decorated over actions that checks whether user has claim for the action being attempted for access.

```csharp
[ClaimsAuthorize(Claim = "Home_Contact")]
public ActionResult Contact()
{
    ViewBag.Message = "Your contact page.";
    return View();
}
```
