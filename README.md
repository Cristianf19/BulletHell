flowchart LR
  subgraph MainMenuScene["MainMenu Scene"]
    MM[MainMenu.cs / UI Canvas]
    MenuMgr[MenuManager.cs]
  end

  subgraph GameScene["Game Scene (Runtime)"]
    GM[Game / Level Manager (implicit)]
    Player[Player.cs]
    UI[UI Canvas (HUD, PausePanel)]
    Pool[PoolManager / Pool classes]
    Spawn[SpawnEnemyManager.cs]
    Waves[WaveManager.cs]
    EnemyComp[Enemy.cs]
    Shooter[Shooter.cs]
    ShootPattern[ShootPattern.cs]
    Movement[EnemyMovementPattern*]
    Stats[EnemyStats / PlayerStats]
    Health[HealthManager / HealthBar]
  end

  %% Scene transitions
  MM -- "Play (Load GameScene)" --> GameScene
  MenuMgr -- "ReturnToMenu / Quit" --> MM

  %% Gameplay flows
  Player -- "input -> shoots" --> Shooter
  Shooter -- "uses" --> Pool
  Shooter -- "uses" --> ShootPattern
  EnemyComp -- "has" --> Movement
  EnemyComp -- "uses" --> Shooter
  EnemyComp -- "uses" --> Stats
  EnemyComp -- "uses" --> Health

  Spawn -- "spawns enemies via" --> Pool
  Waves -- "triggers" --> Spawn
  GM -- "orchestrates" --> Spawn
  GM -- "orchestrates" --> Pool
  GM -- "updates game state" --> UI
  UI -- "pause/resume" --> GM
  MenuMgr -- "Pause UI (in GameScene)" --> UI

  %% Pools and reuse
  Pool -- "provides bullets/enemies to" --> Shooter
  Pool -- "provides enemies to" --> Spawn

  %% Notes cluster
  classDef notes fill:#f9f9f9,stroke:#333,stroke-width:0.5;
  subgraph Notes["Notes"]
    N1["- Pause: Time.timeScale = 0 (MenuManager)"]
    N2["- Pools avoid Instantiate/Destroy"]
    N3["- Movement patterns: ZigZag / Straight (inherit EnemyMovementPattern)"]
    N4["- ShootPattern defines firing behaviour"]
  end
  Notes --- GameScene
