flowchart LR

  %% ========== MAIN MENU ==========

  subgraph MainMenuScene["MainMenu Scene"]
    MM["MainMenu.cs / UI"]
    MenuMgr["MenuManager.cs"]
  end


  %% ========== GAME SCENE ==========

  subgraph GameScene["Game Scene"]
    CoreManager["Game Core Manager"]
    Player["Player.cs"]
    UIHUD["HUD Canvas"]
    UIPause["Pause Menu Canvas"]
    Pool["PoolManager.cs"]
    Spawn["SpawnEnemyManager.cs"]
    Waves["WaveManager.cs"]
    EnemyComp["Enemy.cs"]
    Shooter["Shooter.cs"]
    ShootPattern["ShootPattern.cs"]
    MovementPattern["EnemyMovementPattern (Base + Children)"]
    Stats["EnemyStats / PlayerStats"]
    HealthSys["HealthManager + HealthBar"]
  end


  %% ========== SCENE TRANSITIONS ==========

  MM -- "Play" --> GameScene
  MenuMgr -- "ReturnToMenu" --> MM


  %% ========== GAMEPLAY FLOW ==========

  Player --> Shooter
  Shooter --> Pool
  Shooter --> ShootPattern

  EnemyComp --> MovementPattern
  EnemyComp --> Shooter
  EnemyComp --> Stats
  EnemyComp --> HealthSys

  Waves --> Spawn
  Spawn --> Pool

  UIHUD --> CoreManager
  UIPause --> CoreManager
  CoreManager --> UIHUD

  MenuMgr --> UIPause


  %% ========== POOLING ==========

  Pool -- "Bullets" --> Shooter
  Pool -- "Enemies" --> Spawn


  %% ========== NOTES ==========

  subgraph Notes["Notes"]
    N1["Pause = timeScale = 0"]
    N2["Pooling optimiza rendimiento"]
    N3["MovementPatterns: ZigZag, Straight…"]
    N4["ShootPattern define comportamiento del disparo"]
  end

  Notes --- GameScene
