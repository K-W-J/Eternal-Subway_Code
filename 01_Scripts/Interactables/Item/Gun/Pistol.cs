using System.Collections;
using UnityEngine;

namespace _01_Scripts.Interactables.Item.Gun
{
    public class Pistol : PickUpable.PickUpable, IItem
    {
        [field: SerializeField] public int MaxAmmo { get; private set; }
        [field: SerializeField] public int CurrentAmmo { get; private set; }
        public bool IsDisposable { get; set; }
        public bool IsRightNow { get; set; }
        public Player.Player Player { get; set; }
        
        [SerializeField] private ParticleHandler _particleHandler;
        
        [SerializeField] private GameObject _bulletPrefab;
        
        [SerializeField] private Transform _muzzle;

        private bool _isAmining;
        
        private void Start()
        {
            CurrentAmmo = MaxAmmo;
        }
        
        private void Update()
        {
            if(Player == null) return;
            
            if(_isAmining)
                StartCoroutine(WaitAiming(true));
        }
        
        public bool IsAmmoFull() => CurrentAmmo >= MaxAmmo;

        public int AddAmmo(int ammo)
        {
            if(CurrentAmmo < MaxAmmo)
                CurrentAmmo += ammo;

            if (CurrentAmmo - MaxAmmo > 0)
            {
                int remain = CurrentAmmo - MaxAmmo;

                CurrentAmmo -= CurrentAmmo - MaxAmmo;
                
                return remain;
            }

            return 0;
        }

        public void UseItem()
        {
            if (CurrentAmmo <= 0)
            {
                SoundManager.Instance.PlaySFX("EmptyAmmo", transform, false, 0.8f);
                return;
            }

            CameraManager.Instance.CameraGunShaking();
            
            CurrentAmmo--;
            
            _particleHandler.PlayParticle();
            SoundManager.Instance.PlaySFX("GunShoot", transform, false, 0.8f);
            Instantiate(_bulletPrefab, _muzzle.transform.position, _muzzle.transform.rotation * Quaternion.Euler(0, -90, 0));
        }

        public void SecondaryUseItem()
        {
            _isAmining = Player.PlayerUse.IsUsing;
            
            if(!_isAmining)
                StartCoroutine(WaitAiming(false));
        }

        private IEnumerator WaitAiming(bool isAsiming)
        {
            if(Player == null) yield break;
            
            if (isAsiming == true)
            {
                transform.SetParent(Player.AimingPoint);
                
                while (transform.position != Player.AimingPoint.position || Mathf.Approximately(Player.CinemaCamera.Lens.FieldOfView, 30f))
                {
                    
                    if(!Player.PlayerUse.IsUsing) break;
                    
                    yield return null;
                    
                    if(Player == null) yield break;
                    
                    transform.position = Vector3.Lerp(transform.position, Player.AimingPoint.position, Time.deltaTime * 5f);
                    Player.CinemaCamera.Lens.FieldOfView = Mathf.Lerp(Player.CinemaCamera.Lens.FieldOfView, 30f, Time.deltaTime * 2f);
                }
                
                Player.CinemaCamera.Lens.FieldOfView = 30f;
            }
            else
            {
                transform.SetParent(Player.HandPoint);
                
                while (transform.position != Player.HandPoint.position || Mathf.Approximately(Player.CinemaCamera.Lens.FieldOfView, 60f))
                {
                    
                    if(Player.PlayerUse.IsUsing) break;
                    
                    yield return null;
                    
                    if(Player == null) yield break;
                    
                    transform.position = Vector3.Lerp(Player.HandPoint.position, transform.position, Time.deltaTime * 5f); 
                    Player.CinemaCamera.Lens.FieldOfView = Mathf.Lerp(Player.CinemaCamera.Lens.FieldOfView, 60f, Time.deltaTime * 2f);
                }

                Player.CinemaCamera.Lens.FieldOfView = 60f;
            }
        }

        public override void DropObject(Vector3 dropPos)
        {
            transform.position = Player.HandPoint.position;
            Player.CinemaCamera.Lens.FieldOfView = 60f;
            _isAmining = false;
            Player = null;
            
            base.DropObject(dropPos);
        }
    }
}