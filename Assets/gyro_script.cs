using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gyro_script : MonoBehaviour
{
    public GameObject Board_gyro;  // Board responsivo con el giroscopio (sin existencia fisica)
    public GameObject Avatar;      // Camara, vista primera persona
    public GameObject Board;       // Board fisico, acompaña al Avatar

    void Start()
    {
        Input.gyro.enabled = true;  // Activar gyroscopio
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Board_gyro.transform.rotation);
        AplicarRotacionDeGiroscopio();
        Mover();
    }

    void AplicarRotacionDeGiroscopio()
    {
        Board_gyro.transform.rotation = Input.gyro.attitude;
        Board_gyro.transform.Rotate(90f, 90f, 90f, Space.Self);
        Board_gyro.transform.Rotate(90f, 0f, 90f, Space.World);
    }

    void Mover()
    {

        //Caer
        float BoardFall = (Board.transform.rotation.eulerAngles.z);
        BoardFall = (BoardFall > 180) ? BoardFall - 360 : BoardFall;
        if (BoardFall < 0.0f)
        {
            Board.transform.Translate(new Vector3(0.05f, 0.0f, 0.0f)); // Descenzo en inclinacion
        }

        //Eje Z
        float EulerZ = (Board_gyro.transform.rotation.eulerAngles.z);
        EulerZ = (EulerZ > 180) ? EulerZ - 360 : EulerZ;

        if (EulerZ > 10 && EulerZ < 50){
            Board.transform.Translate(new Vector3(0.1f, 0.0f, 0.0f)); // Mover adelante
        }
        if (EulerZ < -10 && EulerZ > -50){
            Board.transform.Translate(new Vector3(-0.1f, 0.0f, 0f)); // Mover atras
        }

        //Eje X
        float EulerX = (Board_gyro.transform.rotation.eulerAngles.x);
        EulerX = (EulerX > 180) ? EulerX - 360 : EulerX;

        if (EulerX > 10 && EulerX < 50){
            Board.transform.Translate(new Vector3(0.0f, 0.0f, -0.08f)); // Mover derecha
        }
        if (EulerX < -10 && EulerX > -50){
            Board.transform.Translate(new Vector3(0.0f, 0.0f, 0.08f)); // Mover izquierda
        }
    }
}
