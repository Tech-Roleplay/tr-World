## Task of <ins>Player</ins>

- Home of the BPlayer Class
- PlayerFunctions



### Functions:
- ```csharp
  void AddMoneyToCash(this BPlayer player, int amount, string reason)
- ```csharp
  void AddMoneyToBank(this BPlayer player, int amount, string reason)
- ```csharp
  void SubMoneyToCash(this BPlayer player, int amount, string reason)
- ```csharp
  void SubMoneyToBank(this BPlayer player, int amount, string reason)
- ```csharp
  void SetMoneyToCash(this BPlayer player, int amount, string reason)
- ```csharp
  void SetMoneyToBank(this BPlayer player, int amount, string reason)
- ```csharp
  int GetMoneyFromCash(this BPlayer player)
  
  return player.CashBalance;
- ```csharp
  int GetMoneyFromBank(this BPlayer player)
  
  return player.BankBalance;
#### Job Functions
- ```csharp
  void SetJob(this BPlayer player, string jobname, int jobgrade)
- ```csharp
  void ChangeDuty(this BPlayer player)
- ```csharp
  void PaymentJob(this BPlayer player)
- ```csharp
  void ResetJob(this BPlayer player)
- ```csharp
  bool IsPlayerBoss(this BPlayer player)
  
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