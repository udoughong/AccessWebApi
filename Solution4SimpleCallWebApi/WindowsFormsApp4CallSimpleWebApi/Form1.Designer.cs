namespace WindowsFormsApp4CallSimpleWebApi
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnGetAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtCreateName = new System.Windows.Forms.TextBox();
            this.BtnCreate = new System.Windows.Forms.Button();
            this.TxtUpdateId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtUpdateName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnUpdate = new System.Windows.Forms.Button();
            this.TxtDeleteId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.ListResult = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // BtnGetAll
            // 
            this.BtnGetAll.Font = new System.Drawing.Font("新細明體", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BtnGetAll.Location = new System.Drawing.Point(12, 12);
            this.BtnGetAll.Name = "BtnGetAll";
            this.BtnGetAll.Size = new System.Drawing.Size(396, 52);
            this.BtnGetAll.TabIndex = 0;
            this.BtnGetAll.Text = "取得資料";
            this.BtnGetAll.UseVisualStyleBackColor = true;
            this.BtnGetAll.Click += new System.EventHandler(this.BtnGetAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(12, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 40);
            this.label1.TabIndex = 1;
            this.label1.Text = "新增名稱";
            // 
            // TxtCreateName
            // 
            this.TxtCreateName.Font = new System.Drawing.Font("新細明體", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtCreateName.Location = new System.Drawing.Point(19, 113);
            this.TxtCreateName.Name = "TxtCreateName";
            this.TxtCreateName.Size = new System.Drawing.Size(183, 55);
            this.TxtCreateName.TabIndex = 2;
            // 
            // BtnCreate
            // 
            this.BtnCreate.Font = new System.Drawing.Font("新細明體", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BtnCreate.Location = new System.Drawing.Point(218, 70);
            this.BtnCreate.Name = "BtnCreate";
            this.BtnCreate.Size = new System.Drawing.Size(190, 98);
            this.BtnCreate.TabIndex = 3;
            this.BtnCreate.Text = "新增資料";
            this.BtnCreate.UseVisualStyleBackColor = true;
            this.BtnCreate.Click += new System.EventHandler(this.BtnCreate_Click);
            // 
            // TxtUpdateId
            // 
            this.TxtUpdateId.Font = new System.Drawing.Font("新細明體", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtUpdateId.Location = new System.Drawing.Point(19, 216);
            this.TxtUpdateId.Name = "TxtUpdateId";
            this.TxtUpdateId.Size = new System.Drawing.Size(183, 55);
            this.TxtUpdateId.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(12, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 40);
            this.label2.TabIndex = 4;
            this.label2.Text = "更新代碼";
            // 
            // TxtUpdateName
            // 
            this.TxtUpdateName.Font = new System.Drawing.Font("新細明體", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtUpdateName.Location = new System.Drawing.Point(19, 319);
            this.TxtUpdateName.Name = "TxtUpdateName";
            this.TxtUpdateName.Size = new System.Drawing.Size(183, 55);
            this.TxtUpdateName.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(12, 276);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 40);
            this.label3.TabIndex = 6;
            this.label3.Text = "更新名稱";
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.Font = new System.Drawing.Font("新細明體", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BtnUpdate.Location = new System.Drawing.Point(218, 174);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(190, 200);
            this.BtnUpdate.TabIndex = 8;
            this.BtnUpdate.Text = "更新資料";
            this.BtnUpdate.UseVisualStyleBackColor = true;
            this.BtnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // TxtDeleteId
            // 
            this.TxtDeleteId.Font = new System.Drawing.Font("新細明體", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtDeleteId.Location = new System.Drawing.Point(19, 420);
            this.TxtDeleteId.Name = "TxtDeleteId";
            this.TxtDeleteId.Size = new System.Drawing.Size(183, 55);
            this.TxtDeleteId.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(12, 377);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(177, 40);
            this.label4.TabIndex = 9;
            this.label4.Text = "刪除代碼";
            // 
            // BtnDelete
            // 
            this.BtnDelete.Font = new System.Drawing.Font("新細明體", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BtnDelete.Location = new System.Drawing.Point(218, 380);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(190, 95);
            this.BtnDelete.TabIndex = 11;
            this.BtnDelete.Text = "刪除資料";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // ListResult
            // 
            this.ListResult.Font = new System.Drawing.Font("新細明體", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ListResult.FormattingEnabled = true;
            this.ListResult.ItemHeight = 40;
            this.ListResult.Location = new System.Drawing.Point(429, 12);
            this.ListResult.Name = "ListResult";
            this.ListResult.Size = new System.Drawing.Size(482, 484);
            this.ListResult.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 508);
            this.Controls.Add(this.ListResult);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.TxtDeleteId);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BtnUpdate);
            this.Controls.Add(this.TxtUpdateName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtUpdateId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnCreate);
            this.Controls.Add(this.TxtCreateName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnGetAll);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnGetAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtCreateName;
        private System.Windows.Forms.Button BtnCreate;
        private System.Windows.Forms.TextBox TxtUpdateId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtUpdateName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnUpdate;
        private System.Windows.Forms.TextBox TxtDeleteId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.ListBox ListResult;
    }
}

