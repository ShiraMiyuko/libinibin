namespace Dargon.LeagueOfLegends {
   public enum CharacterInibinKeyHashes : uint {
      Id = 2921476548U,

      // Skin definitions
      SkinBaseName = 2142495409U,
      SkinBaseSkn = 769344815U,
      SkinBaseTexture = 2640183547U,
      SkinBaseSkl = 1895303501U,

      SkinOneName = 1742464788U,
      SkinOneSkn = 306040338U,
      SkinOneTexture = 664710616U,
      SkinOneSkl = 599757744U,

      SkinTwoName = 1652845395U,
      SkinTwoSkn = 525951185U,
      SkinTwoTexture = 4206390233U,
      SkinTwoSkl = 3575010799U,

      SkinThreeName = 1563226002U,
      SkinThreeSkn = 745862032U,
      SkinThreeTexture = 3453102554U,
      SkinThreeSkl = 2255296558U,

      SkinFourName = 1473606609U,
      SkinFourSkn = 965772879U,
      SkinFourTexture = 2699814875U,
      SkinFourSkl = 935582317U,

      SkinFiveName = 1383987216U,
      SkinFiveSkn = 1185683726U,
      SkinFiveTexture = 1946527196U,
      SkinFiveSkl = 3910835372U,

      SkinSixName = 1294367823U,
      SkinSixSkn = 1405594573U,
      SkinSixTexture = 1193239517U,
      SkinSixSkl = 2591121131U,

      SkinSevenName = 1204748430U,
      SkinSevenSkn = 1625505420U,
      SkinSevenTexture = 439951838U,
      SkinSevenSkl = 1271406890U,

      // Basic stats
      Range = 1387461685U,
      MoveSpeed = 1081768566U,
      ArmorBase = 2599053023U,
      ArmorPerLvl = 1608827366U,
      ManaBase = 742370228U,
      ManaPerLvl = 1003217290U,
      //CritChanceBase,  // These exist in the air game data, but they're 0.0 for everyone
      //CritChancePerLvl,
      ManaRegenBase = 619143803U,
      ManaRegenPerLvl = 1248483905U,
      HealthRegenBase = 4128291318U,
      HealthRegenPerLvl = 3062102972U,
      MagicResistBase = 1395891205U,
      MagicResistPerLvl = 4032178956U,
      HealthBase = 742042233U,
      HealthPerLvl = 3306821199U,
      AttackDamageBase = 1880118880U,
      AttackDamagePerLvl = 1139868982U,
      AttackSpeedBase = 2191293239U,
      AttackSpeedPerLvl = 770205030U,

      // Textures
      SquareTexture = 3606610482U,
      PassiveTexture = 3810483779U,
      CircleTexture = 3392217477U,

      // Sounds
      MoveSound1 = 882852607U,
      MoveSound2 = 882852608U,
      MoveSound3 = 882852609U,
      MoveSound4 = 882852610U,
      ClickSound1 = 789899530U,
      ClickSound2 = 789899531U,
      ClickSound3 = 789899532U,
      ClickSound4 = 789899533U,
      DeathSound = 4123230931U,
      ReadySound = 2896212226U,
      AttackSound1 = 245402728U,
      AttackSound2 = 245402729U,
      AttackSound3 = 245402730U,
      AttackSound4 = 245402731U,
      SpecialSound1 = 2452862586U,
      SpecialSound2 = 2452862585U,

      // Keys for in-game text
      DisplayName = 82690155,
      ChampDescription = 3747042364U,
      PassiveName = 3401798261U,
      PassiveDescription = 743602011U,
      Tips = 70667385U,
      OpposingTips = 70667386U,
      Lore = 4243215483U,

      // Model Particle definitions
      ModelParticles1 = 78882426U,
      ModelParticles2 = 78882427U,
      ModelParticles3 = 78882428U,
      ModelParticles4 = 78882429U,
      ModelParticles5 = 78882430U,
      ModelParticles6 = 78882431U,
      ModelParticles7 = 78882432U,
      ModelParticles8 = 78882433U,
      ModelParticles9 = 78882434U,
      ModelParticles10 = 78882435U,
      ModelParticles11 = 78882436U,
      ModelParticles12 = 78882437U,
      ModelParticles13 = 78882438U,
      ModelParticles14 = 78882439U,
      ModelParticles15 = 78882440U,

      // Base name of spell files. NOT the in-game name. This is the name of the .inibin/.fx/.luaobj files in the DATA/Spells directory
      SpellOneFileName = 404599689U,
      SpellTwoFileName = 404599690U,
      SpellThreeFileName = 404599691U,
      SpellFourFileName = 404599692U,

      BasicAttackOneFileName = 3075090915U, // Some champions have a blank string for this. I'm guessing the file name is assumed since it's usually <champName>BasicAttack
      BasicAttackTwoFileName = 1578136857U,
      BasicAttackThreeFileName = 1578136858U,
      BasicAttackFourFileName = 1578136859U,
      CritAttackFileName = 1637964898U,

      PassiveFileName_Obsolete = 3706924793U,

      SpellParticleEffect1 = 3170278617U, // These don't correspond to a specific ability slot. IE: SpellPArticleEffectThree could be the particles for a champ's Q
      SpellParticleEffect2 = 3170278618U,
      SpellParticleEffect3 = 3170278619U,
      SpellParticleEffect4 = 3170278620U,
      SpellParticleEffect5 = 3170278621U,
      SpellParticleEffect6 = 3170278622U,

      // Misc
      ChampTypeTags = 4146314945U, // EX: Mage, Pusher, Recommended

      // Junk
      Obsolete1 = 11266465U,
      Obsolete2 = 1190673158U,
      Obsolete3 = 2003803037U,
      Obsolete4 = 1639648700U,
      Obsolete5 = 4285410714U,
      Obsolete6 = 4285410715U,
      Obsolete7 = 4285410716U,
      Obsolete8 = 4285410717U,
      Obsolete9 = 43754799U,
      Obsolete10 = 43754800U,
      Obsolete11 = 43754801U,
      Obsolete12 = 43754802U,
      Obsolete13 = 743602012U,
      Obsolete14 = 743602013U,
      Obsolete15 = 743602014U,
      Obsolete16 = 500084411U,
      Obsolete17 = 4219300475U,
      Obsolete18 = 4086322914U,
      Obsolete19 = 3866412067U,
      Obsolete20 = 3655487418U,
      Obsolete21 = 3646501220U,
      Obsolete22 = 3230821742U,
      Obsolete23 = 2779212989U,
      Obsolete24 = 3639466371U,
      Obsolete25 = 3639466372U,
      Obsolete26 = 3639466373U,
      Obsolete27 = 3639466374U,

      // Empty String
      EmptyStringOne = 1159941902U,
      EmptyStringTwo = 1159941903U,
      EmptyStringThree = 1159941904U,

   }
}
