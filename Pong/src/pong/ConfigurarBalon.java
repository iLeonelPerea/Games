/**********************************************************************/
/* Autor: Francisco I. Leyva
 * P�gina web: http://www.panchosoft.com
 * Correo electr�nico: yagami_2@hotmail.com
 *
 *
/**********************************************************************/
package pong;
import javax.swing.*;

public class ConfigurarBalon extends javax.swing.JFrame {
    
    /*Creamos los vectores con las rutas a las imagenes.*/
    
    /*Predeterminado.*/
    private int cantidadBalones = 3;
    public ImageIcon[] balones = new ImageIcon[cantidadBalones];
    public ImageIcon balonElegido;
    public String[] nombreBalones = new String[cantidadBalones];
    public String nombreBalonElegido;
    private Mundo3D padre;

    
    public ConfigurarBalon(Mundo3D mundo) {
        padre = mundo;
        /*Iniciamos variables.*/
        balones[0] = new ImageIcon(getClass().getResource("/pong/images/balonAdidas.jpg"));
        balones[1] = new ImageIcon(getClass().getResource("/pong/images/balonTierra.jpg"));
        balones[2] = new ImageIcon(getClass().getResource("/pong/images/balonLava.jpg"));

        nombreBalones[0]="Adidas";
        nombreBalones[1]="Tierra";
        nombreBalones[2]="Lava";

        balonElegido = balones[0];
        nombreBalonElegido = nombreBalones[0];

        /*Iniciamos componentes visuales.*/
        initComponents();
    }
    
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        lblFicha2 = new javax.swing.JLabel();
        lblInstrucciones = new javax.swing.JLabel();
        cmbElegir = new javax.swing.JComboBox();
        jSeparator1 = new javax.swing.JSeparator();
        jButton1 = new javax.swing.JButton();

        setDefaultCloseOperation(javax.swing.WindowConstants.DISPOSE_ON_CLOSE);
        setTitle("Configuraciones");

        lblFicha2.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/balonAdidas.jpg"))); // NOI18N

        lblInstrucciones.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/rating_star.png"))); // NOI18N
        lblInstrucciones.setText("Seleccione balón:");

        cmbElegir.setModel(new javax.swing.DefaultComboBoxModel(new String[] { "Adidas 2006", "Tierra", "Lava" }));
        cmbElegir.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                cmbElegirActionPerformed(evt);
            }
        });

        jButton1.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/icon_arrow.gif"))); // NOI18N
        jButton1.setText("Aceptar");
        jButton1.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButton1ActionPerformed(evt);
            }
        });

        org.jdesktop.layout.GroupLayout layout = new org.jdesktop.layout.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(org.jdesktop.layout.GroupLayout.LEADING)
            .add(layout.createSequentialGroup()
                .add(layout.createParallelGroup(org.jdesktop.layout.GroupLayout.LEADING)
                    .add(layout.createSequentialGroup()
                        .add(20, 20, 20)
                        .add(lblInstrucciones)
                        .add(19, 19, 19)
                        .add(cmbElegir, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE, 145, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE))
                    .add(layout.createSequentialGroup()
                        .addContainerGap()
                        .add(jSeparator1, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE, 285, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE))
                    .add(layout.createSequentialGroup()
                        .add(111, 111, 111)
                        .add(jButton1))
                    .add(layout.createSequentialGroup()
                        .add(75, 75, 75)
                        .add(lblFicha2)))
                .addContainerGap(org.jdesktop.layout.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(org.jdesktop.layout.GroupLayout.LEADING)
            .add(org.jdesktop.layout.GroupLayout.TRAILING, layout.createSequentialGroup()
                .add(37, 37, 37)
                .add(layout.createParallelGroup(org.jdesktop.layout.GroupLayout.BASELINE)
                    .add(cmbElegir, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE, org.jdesktop.layout.GroupLayout.DEFAULT_SIZE, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE)
                    .add(lblInstrucciones))
                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED, org.jdesktop.layout.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .add(jSeparator1, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE, 10, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
                .add(lblFicha2)
                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
                .add(jButton1)
                .addContainerGap())
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void jButton1ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButton1ActionPerformed
        this.dispose();
    }//GEN-LAST:event_jButton1ActionPerformed

    private void cmbElegirActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_cmbElegirActionPerformed
        int indice = cmbElegir.getSelectedIndex();
        
        if ( indice == 0 ){
            balonElegido = balones[0];
            nombreBalonElegido = nombreBalones[0];
        }else if ( indice == 1 ){
            balonElegido = balones[1];
            nombreBalonElegido = nombreBalones[1];
        }
        else if ( indice == 2 ){
            balonElegido = balones[2];
            nombreBalonElegido = nombreBalones[2];
        }
        lblFicha2.setIcon( balonElegido );
        padre.getPelota().setTextura("images/balon"+nombreBalonElegido+".jpg");
    }//GEN-LAST:event_cmbElegirActionPerformed

    
    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JComboBox cmbElegir;
    private javax.swing.JButton jButton1;
    private javax.swing.JSeparator jSeparator1;
    private javax.swing.JLabel lblFicha2;
    private javax.swing.JLabel lblInstrucciones;
    // End of variables declaration//GEN-END:variables
    
}
