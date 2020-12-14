## Users

### Commands

#### AuthenticateUser

##### Input

* ``string`` Username !
* ``string`` Password !

##### Output

* ``User`` AuthenticatedUser

#### CreateUser

##### Input

* ``string`` Firstname !
* ``string`` Lastname
* ``string`` Username !
* ``string`` Password !
* ``string`` Role !
* ``string`` Email !

##### Output

* ``User`` CreatedUser

#### DeleteUser

##### Input

* ``Guid`` Id !
* ``System.Security.Claims.ClaimsPrincipal`` User !
* ``string`` Password !

#### ResetPassword

##### Input

* ``Guid`` Id !
* ``System.Security.Claims.ClaimsPrincipal`` User !

#### RecoverPassword

##### Input

* ``string`` RecoveryToken !

##### Output

* ``User`` RecoverdUser

### Queries

#### GetAllUsers

##### Input

* ``System.Security.Claims.ClaimsPrincipal`` User !

##### Output

* ``IEnumerable<User>`` Users

#### GetUser

##### Input

* ``Guid`` Id
* ``System.Security.Claims.ClaimsPrincipal`` User !

##### Output

* ``User`` User
