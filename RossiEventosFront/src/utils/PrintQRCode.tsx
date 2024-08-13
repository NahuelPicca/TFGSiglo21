import React from 'react';
import QRCode from 'react-qr-code';

interface PrintQRCodeProps {
    value: string;
}

const PrintQRCode: React.FC<PrintQRCodeProps> = ({ value }) => {
    const handlePrint = () => {
        const printWindow = window.open('', '', 'height=600,width=800');
        if (printWindow) {
            printWindow.document.open();
            printWindow.document.write(`
                <html>
                    <head>
                        <title>Imprimir QR Codesdasdasdasdasdsad</title>
                        <style>
                            body { text-align: center; font-family: Arial, sans-serif; }
                            #qr-code { margin: 20px; }
                        </style>
                    </head>
                    <body>
                        <h1>QR Code</h1>
                        <div id="qr-code">
                            ${document.getElementById('qr-code-to-print')?.innerHTML || ''}
                        </div>
                        <script>
                            window.onload = function() {
                                window.print();
                            }
                        </script>
                    </body>
                </html>
            `);
            printWindow.document.close();
        }
    };

    return (
        <div>
            <div id="qr-code-to-print" style={{ display: 'none' }}>
                <QRCode value={value} />
            </div>
            {/* <button onClick={handlePrint}>Imprimir QR Code</button> */}
        </div>
    );
};

export default PrintQRCode;
