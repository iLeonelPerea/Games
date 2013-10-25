/**********************************************************************/
/* Autor: Francisco I. Leyva
 * P�gina web: http://www.panchosoft.com
 * Correo electr�nico: yagami_2@hotmail.com
 *
 *
/**********************************************************************/
package pong;
import javax.swing.*;

public class About extends javax.swing.JFrame {
       
    public About() {
        /*Iniciamos componentes visuales.*/
        initComponents();
    }
    
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        lblInstrucciones = new javax.swing.JLabel();
        jSeparator1 = new javax.swing.JSeparator();
        jButton1 = new javax.swing.JButton();
        jLabel1 = new javax.swing.JLabel();
        jLabel2 = new javax.swing.JLabel();
        jLabel3 = new javax.swing.JLabel();
        jLabel4 = new javax.swing.JLabel();
        jLabel5 = new javax.swing.JLabel();
        jLabel6 = new javax.swing.JLabel();
        jLabel7 = new javax.swing.JLabel();
        jLabel8 = new javax.swing.JLabel();

        setDefaultCloseOperation(javax.swing.WindowConstants.DISPOSE_ON_CLOSE);
        setTitle("Acerca de...");

        lblInstrucciones.setFont(new java.awt.Font("Tahoma", 0, 14)); // NOI18N
        lblInstrucciones.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/rating_star.png"))); // NOI18N
        lblInstrucciones.setText("Acerca De:");

        jButton1.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/icon_arrow.gif"))); // NOI18N
        jButton1.setText("Aceptar");
        jButton1.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButton1ActionPerformed(evt);
            }
        });

        jLabel1.setFont(new java.awt.Font("Tahoma", 0, 12)); // NOI18N
        jLabel1.setText("Pong 3D");

        jLabel2.setFont(new java.awt.Font("Tahoma", 1, 12)); // NOI18N
        jLabel2.setText("Desarrollado, imágenes y animaciones por:");

        jLabel3.setText("Isaac Contreras Sandoval - 366045");

        jLabel4.setFont(new java.awt.Font("Tahoma", 1, 12)); // NOI18N
        jLabel4.setText("Música:");

        jLabel5.setText("Dash Berlin - Waiting");

        jLabel6.setFont(new java.awt.Font("Tahoma", 1, 12)); // NOI18N
        jLabel6.setText("ITESM Campus Zacatecas");

        jLabel7.setText("Desarrollo de Videojuegos");

        jLabel8.setText("Mayo 2010");

        org.jdesktop.layout.GroupLayout layout = new org.jdesktop.layout.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(org.jdesktop.layout.GroupLayout.LEADING)
            .add(layout.createSequentialGroup()
                .add(layout.createParallelGroup(org.jdesktop.layout.GroupLayout.LEADING)
                    .add(layout.createSequentialGroup()
                        .add(111, 111, 111)
                        .add(jButton1))
                    .add(layout.createSequentialGroup()
                        .addContainerGap()
                        .add(lblInstrucciones)
                        .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
                        .add(jLabel1))
                    .add(layout.createSequentialGroup()
                        .addContainerGap()
                        .add(jSeparator1, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE, 285, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE))
                    .add(layout.createSequentialGroup()
                        .addContainerGap()
                        .add(jLabel2))
                    .add(layout.createSequentialGroup()
                        .addContainerGap()
                        .add(jLabel3))
                    .add(layout.createSequentialGroup()
                        .addContainerGap()
                        .add(jLabel4))
                    .add(layout.createSequentialGroup()
                        .addContainerGap()
                        .add(jLabel5))
                    .add(layout.createSequentialGroup()
                        .addContainerGap()
                        .add(jLabel6))
                    .add(layout.createSequentialGroup()
                        .addContainerGap()
                        .add(jLabel7))
                    .add(layout.createSequentialGroup()
                        .addContainerGap()
                        .add(jLabel8)))
                .addContainerGap(org.jdesktop.layout.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(org.jdesktop.layout.GroupLayout.LEADING)
            .add(org.jdesktop.layout.GroupLayout.TRAILING, layout.createSequentialGroup()
                .addContainerGap()
                .add(layout.createParallelGroup(org.jdesktop.layout.GroupLayout.BASELINE)
                    .add(lblInstrucciones)
                    .add(jLabel1))
                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
                .add(jSeparator1, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE, 10, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
                .add(jLabel2)
                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
                .add(jLabel3)
                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
                .add(jLabel4)
                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
                .add(jLabel5)
                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
                .add(jLabel6)
                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
                .add(jLabel7)
                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
                .add(jLabel8)
                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED, 22, Short.MAX_VALUE)
                .add(jButton1)
                .addContainerGap())
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void jButton1ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButton1ActionPerformed
        this.dispose();
    }//GEN-LAST:event_jButton1ActionPerformed

    
    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JButton jButton1;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JLabel jLabel3;
    private javax.swing.JLabel jLabel4;
    private javax.swing.JLabel jLabel5;
    private javax.swing.JLabel jLabel6;
    private javax.swing.JLabel jLabel7;
    private javax.swing.JLabel jLabel8;
    private javax.swing.JSeparator jSeparator1;
    private javax.swing.JLabel lblInstrucciones;
    // End of variables declaration//GEN-END:variables
    
}
