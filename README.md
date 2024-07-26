# ⚔ Life Regeneration in Bannerlord!

- [Steam Bannerlord Workshop Link](https://steamcommunity.com/sharedfiles/filedetails/?id=3190267630)

Life regeneration mod for Bannerlord Mount and Blade 2.

This module gives play style of other RPG game with life regeneration.

Life regeneration is only applied to the player and player's mount during the mission (Battle, Dual or any kind of player's mission type, even barter..). And having higher endurance increase the life regeneration amount.

You can configure the life regeneration multiplier from config.json file that is located in 'My Document/LifeRegen' Folder. Make sure only adjust number like 0.01 ~ 10.0 etc. If you want strong healing that you almost don't die, you can just 10.0. The default is 1.0.

Currently I am developing my 2D game. So it is difficult for me to work on GUI menu. It is probably almost impossible for me to update it. Because there is not much documentation and Bannerlord's System is very complicated since they use their own game engine. So please understand how things are not going to be updated.

## 👨‍🏫 Notice & Change Note

From Mod version 1.0.2, you can configure amount of healing from config.json file that is located in 'My Document/LifeRegen' folder.

## 📖 Calculation

The calculation is '(total health point / 100) \* endurance_multiplier' per second

So default amount is 1% of life regeneration.

And the calculation endurance_multiplier is 'endurance points / 2'.

So every 2 endurance you will get 1%. But 2% start from 4 endurance.

Maximum is 100%. But in that case you need 200 endurance.

In custom battle, life regeneration will be applied without endurance modifier.

And the mount will also get benefits of endurance too.

## 😵 Menu

This module does not support MCM or menu. Because UI system is not familiar to me and it is quite complicated + not much documentation or tutorial.

## 🛠 Compatibility

This module operates independently and does not rely on other community modules.

So this module should be compatible with any other module and any other version of bannerlord. Unless native bannerlord's API is different from v1.2.9 or other module is affecting Mission.MainAgent and the mount.

## 💪 Support LifeRegen

### 👼 Become a Sponsor

- [Ko-fi](https://ko-fi.com/opdev1004)
- [Github sponsor page](https://github.com/sponsors/opdev1004)

### 🎁 Shop

- [RB Geargom Shop](https://www.redbubble.com/people/Geargom/shop)

## 👨‍💻 Author

[Victor Chanil Park](https://github.com/opdev1004)

## 💯 License

MIT, See [LICENSE](./LICENSE).
