## Task of <ins>Player</ins>

- Home of the TPlayer Class
- PlayerFunctions

### Functions:

- ```csharp
  void AddMoneyToCash(this TPlayer player, int amount, string reason)
- ```csharp
  void AddMoneyToBank(this TPlayer player, int amount, string reason)
- ```csharp
  void SubMoneyToCash(this TPlayer player, int amount, string reason)
- ```csharp
  void SubMoneyToBank(this TPlayer player, int amount, string reason)
- ```csharp
  void SetMoneyToCash(this TPlayer player, int amount, string reason)
- ```csharp
  void SetMoneyToBank(this TPlayer player, int amount, string reason)
- ```csharp
  int GetMoneyFromCash(this TPlayer player)
  
  return player.CashBalance;
- ```csharp
  int GetMoneyFromBank(this TPlayer player)
  
  return player.BankBalance;

#### Job Functions

- ```csharp
  void SetJob(this TPlayer player, string jobname, int jobgrade)
- ```csharp
  void ChangeDuty(this TPlayer player)
- ```csharp
  void PaymentJob(this TPlayer player)
- ```csharp
  void ResetJob(this TPlayer player)
- ```csharp
  bool IsPlayerBoss(this TPlayer player)
  
  return player.Job.IsBoss;
- ```csharp
  string GetPlayerJobType(this BPlayer player)
  
  return player.Job.Type;
- ```csharp
  string GetSkinForJob(this BPlayer player)
  
  return player.Job.Skin_Male;
  return player.Job.Skin_Female;
  return string.Empty;

#### Gang Functions

#### Skin Functions

- ````csharp
  void SetCloth(this BPlayer player, ComponentIDs componet, int drawable, int texture, int? pallette)
- ```csharp
  string GetCloth(this BPlayer player)
  
  //temp return "Cloth";
  return "Cloth"
- ```csharp
  void SetDlcCloth(this BPlayer player, int dlc, ComponentIDs componet, int drawable, int texture, int? pallette)