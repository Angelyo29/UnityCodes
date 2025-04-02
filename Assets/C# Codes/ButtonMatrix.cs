using UnityEngine;
using System;
using static System.Math;
using UnityEngine.UI;
using TMPro;

public class ButtonMatrix : MonoBehaviour
{
    [SerializeField] private Button ButtonAxis;
    [SerializeField] private Animator animator;
    [SerializeField] private Color color1 = Color.red;
    [SerializeField] private Color color2 = Color.green;
    [SerializeField] private Color color3 = Color.white;
    [SerializeField] private Slider sliderVerificator;
    [SerializeField] private GameObject panel;
    //[SerializeField] private TextMeshProUGUI sliderText = null;

    private int n = 0;

    void Start()
    {
        ButtonAxis.onClick.AddListener(ChangeState);
        if (animator == null)
            animator = GetComponent<Animator>();
        if (ButtonAxis == null)
            ButtonAxis = GetComponent<Button>();
    }

    public void ChangeState()
    {
        switch (SliderGlobal.value+1)
        {
            case 0:
                ButtonAxis.image.color = color1;
                animator.SetTrigger("FirstSelect");
                //Debug.Log("Activando color rojo y animación FirstSelect");
                break;
            case 1:
                ButtonAxis.image.color = color2;
                animator.SetTrigger("SecondSelect");
                //Debug.Log("Activando color verde y animación SecondSelect");

                float a1_val = 71.63f;
                float d1_val = 29.6f;
                float T1 = 0.5f;
                float off = 0.1f;

                Matrix4x4 H1 = DenavitMatrix(a1_val, T1 + off, d1_val, Mathf.PI / 2);

                panel.SetActive(true);
                LogMatrix(H1);
                break;
            case 2:
                ButtonAxis.image.color = color3;
                animator.SetTrigger("Reset");
                Debug.Log("Activando color azul y animación Reset");
                n = -1;
                panel.SetActive(false);
                break;
        }
        n++;
        Debug.Log($"Estado actual: {n}");
    }

    private Matrix4x4 DenavitMatrix(float a_i, float theta, float d_i, float alpha)
    {
        float cosTheta = Mathf.Cos(theta);
        float sinTheta = Mathf.Sin(theta);
        float cosAlpha = Mathf.Cos(alpha);
        float sinAlpha = Mathf.Sin(alpha);

        Matrix4x4 matrix = new Matrix4x4();
        matrix.m00 = cosTheta;
        matrix.m01 = -cosAlpha * sinTheta;
        matrix.m02 = sinAlpha * sinTheta;
        matrix.m03 = a_i * cosTheta;

        matrix.m10 = sinTheta;
        matrix.m11 = cosAlpha * cosTheta;
        matrix.m12 = -sinAlpha * cosTheta;
        matrix.m13 = a_i * sinTheta;

        matrix.m20 = 0;
        matrix.m21 = sinAlpha;
        matrix.m22 = cosAlpha;
        matrix.m23 = d_i;

        matrix.m30 = 0;
        matrix.m31 = 0;
        matrix.m32 = 0;
        matrix.m33 = 1;

        return matrix;
    }

    private void LogMatrix(Matrix4x4 matrix)
    {
        string matrixString = "Matriz de Transformación de Denavit-Hartenberg:\n";
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                matrixString += matrix[i, j].ToString("F4") + "\t";
            }
            matrixString += "\n";
        }
        Debug.Log(matrixString);
    }
}