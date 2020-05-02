using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : IEnumerator, IEnumerable
{
    GameObject Player;
    GameObject gameManager;
    static Dictionary<EquipmentType, EquipmentData> equipment =
        new Dictionary<EquipmentType, EquipmentData>();
    static Dictionary<EquipmentHand, EquipmentType> equipped =
        new Dictionary<EquipmentHand, EquipmentType>();
    private static EquipmentType Equipped(EquipmentHand hand)
    {
        if (equipped.ContainsKey(hand)) return equipped[hand];
        else return EquipmentType.None;
    }
    public static EquipmentType Primary => Equipped(EquipmentHand.Primary);
    public static EquipmentType Secondary => Equipped(EquipmentHand.Secondary);
    public static int Price(EquipmentType type) 
    {
        if (type == EquipmentType.None) return 0;
        return equipment[type].price;
    }

    public static Dictionary<EquipmentType, EquipmentData> Eq => equipment;
    public void Initialize()
    {
        equipment = new Dictionary<EquipmentType, EquipmentData>();
        equipped = new Dictionary<EquipmentHand, EquipmentType>();
    }
    public bool MoveNext()
    {
        return equipment.GetEnumerator().MoveNext();
    }
    public void Reset()
    {
        equipment.GetEnumerator();
    }
    public object Current => equipment.GetEnumerator().Current;
    public EquipmentData this[EquipmentType equipmentType]
    {
        get => equipment[equipmentType];
        set 
        {
            equipment[equipmentType] = value;
        }
    }
    public int Count => equipment.Count;
    public IEnumerator GetEnumerator()
    {
        return equipment.GetEnumerator();
    }
    public void Initialize(GameObject player)
    {
        Player = player;
    }
    public void Add(EquipmentType type, int price,
        EquipmentStatus status = EquipmentStatus.NotAvailable)
    {
        equipment.Add(type, new EquipmentData
        {
            type = type,
            name = GameConstants.EquipmentNames[type],
            description = GameConstants.EquipmentDesciptions[type],
            status = status,
            prefab = GameConstants.EquipmentPrefab[type],
            price = price,
            hand = GameConstants.EquipmentHands[type]
        }); ;
    }
    public void Equip(EquipmentType type)
    {
        if (equipment[type].status == EquipmentStatus.Owned)
        {
            EquipmentData newData;
            if (equipped.ContainsKey(equipment[type].hand))
            {
                newData = equipment[equipped[equipment[type].hand]];
                newData.status = EquipmentStatus.Owned;
                equipment[equipped[equipment[type].hand]] = newData;
                equipped[equipment[type].hand] = type;
            }
            else equipped.Add(equipment[type].hand, type);
            newData = equipment[type];
            newData.status = EquipmentStatus.Equipped;
            equipment[type] = newData;
        }
    }
    public void ChangeStatus(EquipmentType type, EquipmentStatus newStatus)
    {
        EquipmentData data = equipment[type];
        switch (data.status)
        {
            case EquipmentStatus.Available:
                break;
            case EquipmentStatus.Owned:
                if (newStatus == EquipmentStatus.Equipped) Equip(type);
                break;
        }
        data.status = newStatus;
        equipment[type] = data;
        EventManager.Invoke(EventNames.EquipmentStatusChanged);
    }
    private void Shoot(EquipmentType type, Vector3 position)
    {
        if (equipment[type].hand == EquipmentHand.Primary ||
            GameManager.SpendGold(equipment[type].price))
        {
            if (type == EquipmentType.Laser)
            {
                GameObject bullet = Pools.Get(PoolsEnum.PlayerBullet);
                bullet.transform.position = position;
                bullet.GetComponent<PlayerBullet>().Fire(GameConstants.PlayerBulletAngleDegrees);
            }
            else if (type == EquipmentType.TripleLaser)
            {
                GameObject bullet = Pools.Get(PoolsEnum.PlayerBullet);
                Vector2 bulletPosition = position;
                bullet.transform.position = bulletPosition;
                bullet.GetComponent<PlayerBullet>().Fire(GameConstants.PlayerBulletAngleDegrees);
                bullet = Pools.Get(PoolsEnum.PlayerBullet);
                bulletPosition.y += GameConstants.TripleLaserPositionDelta;
                bullet.transform.position = bulletPosition;
                bullet.GetComponent<PlayerBullet>().Fire(GameConstants.PlayerBulletAngleDegrees);
                bullet = Pools.Get(PoolsEnum.PlayerBullet);
                bulletPosition.y -= 2 * GameConstants.TripleLaserPositionDelta;
                bullet.transform.position = bulletPosition;
                bullet.GetComponent<PlayerBullet>().Fire(GameConstants.PlayerBulletAngleDegrees);
            }
            else if (type == EquipmentType.FanLaser)
            {
                GameObject bullet = Pools.Get(PoolsEnum.PlayerBullet);
                Vector2 bulletPosition = position;
                bullet.transform.position = bulletPosition;
                bullet.GetComponent<PlayerBullet>().Fire(GameConstants.PlayerBulletAngleDegrees);
                bullet = Pools.Get(PoolsEnum.PlayerBullet);
                bulletPosition.y += GameConstants.FanLaserPositionDelta;
                bullet.transform.position = bulletPosition;
                bullet.GetComponent<PlayerBullet>().Fire(GameConstants.FanBulletAngleDegrees);
                bullet = Pools.Get(PoolsEnum.PlayerBullet);
                bulletPosition.y -= 2 * GameConstants.FanLaserPositionDelta;
                bullet.transform.position = bulletPosition;
                bullet.GetComponent<PlayerBullet>().Fire(-GameConstants.FanBulletAngleDegrees);

            }
            else if (type == EquipmentType.BackLaser)
            {
                GameObject bullet = Pools.Get(PoolsEnum.PlayerBullet);
                Vector2 bulletPosition = position;
                bullet.transform.position = bulletPosition;
                bullet.GetComponent<PlayerBullet>().Fire(GameConstants.PlayerBulletAngleDegrees);
                bullet = Pools.Get(PoolsEnum.PlayerBullet);
                bulletPosition += GameConstants.BackLaserPositionDelta;
                bullet.transform.position = bulletPosition;
                bullet.GetComponent<PlayerBullet>().Fire(GameConstants.BackBulletAngleDegrees);
                bullet = Pools.Get(PoolsEnum.PlayerBullet);
                bulletPosition.y -= 2 * GameConstants.BackLaserPositionDelta.y;
                bullet.transform.position = bulletPosition;
                bullet.GetComponent<PlayerBullet>().Fire(-GameConstants.BackBulletAngleDegrees);
            }
            else if (type == EquipmentType.KillerShield)
            {
                GameObject shield = (GameObject)Object.Instantiate(
                    Resources.Load(GameConstants.ShieldPrefab), Player.transform);
                shield.GetComponent<Shield>().Initialize(GameConstants.ShieldHP);
            }
            else if (type == EquipmentType.StrongShield)
            {
                GameObject shield = (GameObject)Object.Instantiate(
                    Resources.Load(GameConstants.StrongShieldPrefab), Player.transform);
                shield.GetComponent<Shield>().Initialize(GameConstants.StrongShieldHP);
            }
            else if (type == EquipmentType.Bomb)
            {
                Bomb bomb = ((GameObject)Object.Instantiate(
                    Resources.Load(GameConstants.BombPrefab))).GetComponent<Bomb>();
                bomb.transform.position = position;
                bomb.Initialize(GameConstants.BombTriggersOnQuantity,
                    GameConstants.BombLaserAngleStep, GameConstants.BombLaserIntensity);
            }
            else if (type == EquipmentType.TripleBomb)
            {
                Bomb bomb = ((GameObject)Object.Instantiate(
                    Resources.Load(GameConstants.TripleBombPrefab))).GetComponent<Bomb>();
                bomb.transform.position = position;
                bomb.Initialize(GameConstants.TripleBombTriggersOnQuantity,
                    GameConstants.BombLaserAngleStep, GameConstants.TripleBombLaserIntensity);
            }
        }
    }
    public void Shoot(EquipmentHand hand, Vector3 position)
    {
        if (equipped.ContainsKey(hand) && equipped[hand] != EquipmentType.None) 
            Shoot(equipped[hand], position);
    }
}
