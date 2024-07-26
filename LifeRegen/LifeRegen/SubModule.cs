
using System;
using System.IO;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using Newtonsoft.Json;

namespace LifeRegen
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

			string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			string liferegenUserFolder = Path.Combine(documentsFolder, "LifeRegen");
			string configFilePath = Path.Combine(liferegenUserFolder, "config.json");

			if (!File.Exists(configFilePath))
			{
				Directory.CreateDirectory(liferegenUserFolder);
				LifeRegenConfig configForWrite = new LifeRegenConfig
				{
					RegenMultiplier = 1.0f
				};

				string jsonStringForWrite = JsonConvert.SerializeObject(configForWrite);
				File.WriteAllText(configFilePath, jsonStringForWrite);
			}
		}

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);
            InformationManager.DisplayMessage(new InformationMessage("[opdev1004] Thank you for using LifeRegen mod!"));
        }

        public override void OnMissionBehaviorInitialize(Mission mission)
        {
            base.OnMissionBehaviorInitialize(mission);
			string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			string liferegenUserFolder = Path.Combine(documentsFolder, "LifeRegen");
			string configFilePath = Path.Combine(liferegenUserFolder, "config.json");

			if (File.Exists(configFilePath))
			{
				string jsonStringForRead = File.ReadAllText(configFilePath);
				LifeRegenConfig configForRead = JsonConvert.DeserializeObject<LifeRegenConfig>(jsonStringForRead);
				LifeRegenMissionBehavior mb = new LifeRegenMissionBehavior
				{
					userValue = configForRead.RegenMultiplier
				};
				mission.AddMissionBehavior(mb);
			}
            else
            {
				LifeRegenMissionBehavior mb = new LifeRegenMissionBehavior
				{
					userValue = 1.0f
				};
				mission.AddMissionBehavior(mb);
			}
        }
    }

    public class LifeRegenMissionBehavior : MissionBehavior
    {
        private float timer = 0;
        private float endptsMultiplier = 1;
        public float userValue = 1.0f;
        public override MissionBehaviorType BehaviorType => MissionBehaviorType.Other;

        public override void OnBehaviorInitialize()
        {
            base.OnBehaviorInitialize();


            if (Game.Current.GameType is Campaign)
            {
                float endpts = (float)Hero.MainHero.GetAttributeValue(DefaultCharacterAttributes.Endurance);
                endptsMultiplier = (float)Math.Ceiling(endpts / 2);

                if (endptsMultiplier < 1)
                {
                    endptsMultiplier = 1;
                }
                else if (endptsMultiplier > 100)
                {
                    endptsMultiplier = 100;
                }
            }
            else
            {
                endptsMultiplier = 1;
            }
        }

        public override void OnMissionTick(float dt)
        {
            base.OnMissionTick(dt);
            timer += dt;

            if (timer >= 1.0f)
            {
                if (Mission != null && Mission.MainAgent != null)
                {
                    float playerRegenPts = (Mission.MainAgent.HealthLimit / 100) * (endptsMultiplier * userValue);

                    if (Mission.MainAgent.Health < Mission.MainAgent.HealthLimit)
                    {
                        if (Mission.MainAgent.Health + playerRegenPts > Mission.MainAgent.HealthLimit)
                        {
                            Mission.MainAgent.Health = Mission.MainAgent.HealthLimit;
                        }
                        else
                        {
                            Mission.MainAgent.Health += playerRegenPts;
                        }
                    }

                    if (Mission.MainAgent.HasMount)
                    {
                        float mountRegenPts = (Mission.MainAgent.MountAgent.HealthLimit / 100) * (endptsMultiplier * userValue);

						if (Mission.MainAgent.MountAgent.Health < Mission.MainAgent.MountAgent.HealthLimit)
                        {
                            if (Mission.MainAgent.MountAgent.Health + mountRegenPts > Mission.MainAgent.MountAgent.HealthLimit)
                            {
                                Mission.MainAgent.MountAgent.Health = Mission.MainAgent.MountAgent.HealthLimit;
                            }
                            else
                            {
                                Mission.MainAgent.MountAgent.Health += mountRegenPts;
                            }
                        }
                    }
                }

                timer = 0.0f;
            }
        }
    }
}
