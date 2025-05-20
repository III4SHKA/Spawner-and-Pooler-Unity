# Object Pooling Spawner for Unity

This is a simple and efficient object pooling system with a spawner for Unity.  
Object pooling improves performance by reusing game objects instead of instantiating and destroying them repeatedly.

## ✨ Features

- Object pooling system (`ObjectPooler`)  
- Easy-to-use spawner (`Spawner`)  
- Pre-instantiates objects to avoid runtime lags  
- Improves performance on mobile and low-end devices  
- Lightweight and minimalistic design  

## 📦 How It Works

1. Add your prefabs to the `objects` list in the `Spawner` script.
2. Set how many instances of each prefab to pre-instantiate.
3. Objects will be spawned at regular intervals using the pool.

The pool ensures that objects are reused instead of being created and destroyed every time, reducing garbage collection and improving frame rate stability.

## 📂 Structure

- `Spawner.cs`: Controls when and where to spawn objects.
- `ObjectPooler.cs`: Manages inactive objects and reuses them when needed.

## 🚀 Usage

1. Attach the `ObjectPooler` script to a GameObject in your scene (e.g., an empty "PoolManager").
2. Attach the `Spawner` script to a spawner GameObject.
3. Assign your prefabs to the `objects` list in the `Spawner` component via the Inspector.
4. Set the spawn interval and amount of preloaded objects.

## 💬 Community

Join our Ukrainian 🇺🇦 GameDev community for support, questions, and updates:

👉 [GameDevHubUA Telegram Channel](https://t.me/GameDevHubUA)

## 🧠 License

MIT License. Free to use in commercial and personal projects. A star ⭐ on GitHub is always appreciated if you find it useful!

---

Happy coding and smooth spawning!
