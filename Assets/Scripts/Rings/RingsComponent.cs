using UnityEngine;
using UnityEngine.Events;

public class RingsComponent : MonoBehaviour
{
    [SerializeField] private int _rings;
    [SerializeField] private Transform _ringsSpawnPoint;
    [SerializeField] private UnityEvent _onRingsReduce;
    [SerializeField] private UnityEvent _onRingsEnd;
    [SerializeField] private GameObject _dropRings;
    public int Rings
    {   get 
        {
            return _rings;
        }
        set 
        {
            _rings = value;
            OnRingsChange?.Invoke();
        }  
    }
    public delegate void OnChange();
    public event OnChange OnRingsChange;
    public void ApplyRingsChange(int value)
    {
        
         if (value < 0)
         {
            if(Rings<=0)
            {
                _onRingsEnd?.Invoke();
            }
            _onRingsReduce?.Invoke();
            SpawnCoins(value);
            return;
         }
        Rings += value;
    }
    public void SpawnCoins(int valueToSpawn)
    {
        valueToSpawn = -valueToSpawn;
        var numCoinsToDispose = Mathf.Min(_rings, valueToSpawn);
        Rings -= numCoinsToDispose;

        for(int i = 0; i<numCoinsToDispose; i++)
        {
            var instantinate = Instantiate(_dropRings, _ringsSpawnPoint.transform.position, Quaternion.identity);
            var rigidBody = instantinate.GetComponent<Rigidbody2D>();

            var randomForceX = Random.Range(-10,10);
            var randomForceY = Random.Range(-5, 25);

            var randomForce = new Vector2(randomForceX, randomForceY);
            rigidBody.AddForce(randomForce, ForceMode2D.Impulse);
        }
    }
}
